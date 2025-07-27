using System;
using System.Collections;
using UnityEngine;

public class RocketView : MonoBehaviour
{
    public event Action<bool> OnRocketCollided;
    public event Action OnMoveLeft;
    public event Action OnMoveRight;
    public event Action OnAttackButtonPressed;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private RocketSound[] _sound;
    [SerializeField] private int _rotationSpeed = 100;
    [SerializeField] private GameObject _attackSpawner;
    [SerializeField] private GameObject _swipeController;
    [SerializeField] private Animator _animator;

    private AttackSpawner attackSpawner;
    private SwipeController swipeController;
    public Attack attack;


    private bool _isMoving = false;
    private bool _isAttackOnCooldown = true;
    private bool _isInAttackState = false;
    private bool _isForceFieldActive = false;
    private int _isGameStartedHashed;

    private int ShootSoundIndex = 0;
    private int CollisionSoundIndex = 1;
    private int ProtectionReveilSoundIndex = 3;
    private int _isAttackOnCooldownHashed;
    private int _isForceFieldActiveHashed;
    private float _attackCooldown = 0.5f;

    public void Setup()
    {
        swipeController = _swipeController.GetComponent<SwipeController>();
        attackSpawner = _attackSpawner.GetComponent<AttackSpawner>();
        attackSpawner.SetupAttack();
        attack = attackSpawner.attack;
        _isAttackOnCooldownHashed = Animator.StringToHash("isAttackOnCooldown");
        _isForceFieldActiveHashed = Animator.StringToHash("isForceFieldActive");
        _isGameStartedHashed = Animator.StringToHash("isPaused");
    }

    public void StartGame()
    {
        StartCoroutine(AttackCooldown());
        TransferAnimData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !_isMoving)
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !_isMoving)
            MoveRight();
        else if ((Input.GetKeyDown(KeyCode.Space)) && !_isAttackOnCooldown)
        {
            _isInAttackState = true;
            if (!_isMoving)
                OnAttackButtonPressed?.Invoke();
        }
    }

    public void Move(Vector3 targetPosition, float speed, float targetAngle) => StartCoroutine(MoveRoutine(targetPosition, speed, targetAngle));

    private void MoveLeft() => OnMoveLeft?.Invoke();

    private void MoveRight() => OnMoveRight?.Invoke();

    public void Attack(Vector3 rocketPosition)
    {
        attackSpawner.ActivateAttack(rocketPosition);
        PlaySound(ShootSoundIndex);
        StartCoroutine(AttackCooldown());
        _isInAttackState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            OnRocketCollided?.Invoke(false);
        }
        else if (collision.gameObject.TryGetComponent<UFOAttack>(out UFOAttack UFOAttack))
        {
            OnRocketCollided?.Invoke(true);
            PlaySound(CollisionSoundIndex);
        }
    }

    private IEnumerator MoveRoutine(Vector3 targetPosition, float speed, float targetAngle)
    {
        if (Time.timeScale >= 1)
        {
            Vector3 middlePoint = (transform.position + targetPosition) / 2;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            Quaternion reverseTargetRotation = Quaternion.Euler(0, 0, 0);
            _isMoving = true;
            while (Vector3.Distance(transform.position, middlePoint) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, middlePoint, speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, reverseTargetRotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
            _isMoving = false;
            if (_isInAttackState)
                OnAttackButtonPressed?.Invoke();
        }
        else
            Debug.Log("Incorrect time scale");
    }

    private IEnumerator AttackCooldown()
    {
        if (Time.timeScale >= 1)
        {
            _isAttackOnCooldown = true;
            TransferAnimData();
            var wait = new WaitForSeconds(_attackCooldown);
            yield return wait;
            _attackCooldown = 5;
            _isAttackOnCooldown = false;
            TransferAnimData();
        }
        else
            Debug.Log("Incorrect time scale");
    }

    public void RestartGame(Vector3 defaultPosition)
    {
        transform.position = defaultPosition;
        StopAllCoroutines();
        ResetStates();
        attackSpawner.DeactivateAttack();
        TransferAnimData();
    }

    private void ResetStates()
    {
        _isMoving = false;
        _isAttackOnCooldown = true;
        _isInAttackState = false;
        _isForceFieldActive = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _attackCooldown = 0.5f;
    }

    private void PlaySound(int clipIndex)
    {
        _audioSource.clip = _sound[clipIndex].Clip;
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    public void SetCollisionSound(bool isGameOver)
    {
        if (isGameOver)
            PlaySound(CollisionSoundIndex);
        else
            PlaySound(ProtectionReveilSoundIndex);
    }

    private void OnEnable()
    {
        swipeController.OnSwipedLeft += MoveLeft;
        swipeController.OnSwipedRight += MoveRight;
        swipeController.OnTaped += ImitateAttackButtonPressed;
    }

    private void OnDisable()
    {
        swipeController.OnSwipedLeft -= MoveLeft;
        swipeController.OnSwipedRight -= MoveRight;
        swipeController.OnTaped -= ImitateAttackButtonPressed;
    }

    public void ActivateForceField()
    {
        _isForceFieldActive = true;
        TransferAnimData();
    } 
    public void DeactivateForceField()
    {
        _isForceFieldActive = false;
        TransferAnimData();
    }

    public void BeginAnimation() => _animator.SetBool(_isGameStartedHashed, false);

    public void StopAnimation() => _animator.SetBool(_isGameStartedHashed, true);
    private void TransferAnimData()
    {
        if (_isAttackOnCooldown == false)
            _animator.SetBool(_isAttackOnCooldownHashed, false);
        else
            _animator.SetBool(_isAttackOnCooldownHashed, true);

        if (_isForceFieldActive == false)
            _animator.SetBool(_isForceFieldActiveHashed, false);
        else
            _animator.SetBool(_isForceFieldActiveHashed, true);
    }

    private void ImitateAttackButtonPressed()
    {
        if (!_isAttackOnCooldown)
        {
            _isInAttackState = true;
            if (!_isMoving)
                OnAttackButtonPressed?.Invoke();
        }
    }
}
