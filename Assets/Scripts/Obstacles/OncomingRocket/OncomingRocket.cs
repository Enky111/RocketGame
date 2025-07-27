using System;
using System.Collections;
using UnityEngine;

public class OncomingRocket : Obstacle, IDestroyable
{
    [SerializeField] private Animator _animator;

    private Collider2D _collider;
    private bool _isDestroyed = false;
    private int isDestroyedHashed;
    private void Start()
    {
        _speed = 30f;
        _collider = GetComponent<Collider2D>();
        isDestroyedHashed = Animator.StringToHash("isDestroyed");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Attack>(out Attack attack))
            StartCoroutine(Destroying());
    }

    private void TransferAnimData()
    {
        if (_isDestroyed)
            _animator.SetBool(isDestroyedHashed, true);
        else if (_isDestroyed == false)
            _animator.SetBool(isDestroyedHashed, false);
    }
    private IEnumerator Destroying()
    {
        if (Time.timeScale >= 1)
        {
            for (int i = 0; i < 1; i++)
            {
                _isDestroyed = true;
                TransferAnimData();
                _speed = 0f;
                _collider.enabled = false;
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
            _collider.enabled = true;
            _isDestroyed = false;
            TransferAnimData();
            _speed = 30f;
            yield return new WaitForSeconds(0.05f);
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
