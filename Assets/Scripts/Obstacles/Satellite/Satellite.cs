using System;
using System.Collections;
using UnityEngine;

public class Satellite : Obstacle, IDestroyable
{
    [SerializeField] private Animator _animator;

    private bool _isDestroyed = false;
    private int isDestroyedHashed;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        isDestroyedHashed = Animator.StringToHash("isDestroyed");
    }
    private void TransferAnimData()
    {
        if (_isDestroyed)
            _animator.SetBool(isDestroyedHashed, true);
        else if (_isDestroyed == false)
            _animator.SetBool(isDestroyedHashed, false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Attack>(out Attack attack))
            StartCoroutine(Destroying());
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
