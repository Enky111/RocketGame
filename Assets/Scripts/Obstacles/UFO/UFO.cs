using UnityEngine;
using System.Collections;
using System;

public class UFO : Obstacle, IDestroyable
{
    [SerializeField] GameObject _UFOAttackProjectilePrefab;
    [SerializeField] private Animator _animator;

    private bool _isDestroyed = false;
    private int isDestroyedHashed;
    private Collider2D _collider;

    private GameObject _UFOAttackProjectile;
    private UFOAttack _UFOAttack;
    private Vector3 _UFOAttackTargetPosition = new(0f, -17f, 0f);

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        isDestroyedHashed = Animator.StringToHash("isDestroyed");
        if (_isDestroyed)
            StartCoroutine(ResetingView());
        else
            return;
    }

    private void TransferAnimData()
    {
        if (_isDestroyed)
            _animator.SetBool(isDestroyedHashed, true);
        else
            _animator.SetBool(isDestroyedHashed, false);
    }

    public void SetupUFO()
    {
        _UFOAttackProjectile = Instantiate(_UFOAttackProjectilePrefab);
        _UFOAttackProjectile.SetActive(false);
        _UFOAttack = _UFOAttackProjectile.GetComponent<UFOAttack>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Attack>(out Attack attack))
            StartCoroutine(Destroying());
    }

    private void CouterAttack()
    {
        _UFOAttackProjectile.transform.position = transform.position;
        _UFOAttackProjectile.SetActive(true);
        _UFOAttack.SetTargetPosition(transform.position, _UFOAttackTargetPosition);
    }

    private IEnumerator Destroying()
    {
        if (Time.timeScale >= 1)
        {
            for (int i = 0; i < 1; i++)
            {
                _isDestroyed = true;
                TransferAnimData();
                _collider.enabled = false;
                CouterAttack();
                yield return new WaitForSeconds(0.4f);
                InvokeRemovingAction();
            }
        }
        else
            Debug.Log("Incorrect time scale");
    }

    private IEnumerator ResetingView()
    {
        if (Time.timeScale >= 1)
        {
            for (int i = 0; i < 1; i++)
            {
                _collider.enabled = true;
                _isDestroyed = false;
                TransferAnimData();
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
            Debug.Log("Incorrect time scale");
    }
    public void ResetView()
    {
        if (_isDestroyed)
            StartCoroutine(ResetingView());
        else
            return;
    }
}
