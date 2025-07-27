using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public event Action OnUFODestroyed;
    public event Action OnObstacleDestroyed;

    private Vector3 _defaultPosition = new(0f, 30f, -2);
    private Vector3 _targetPosition;
    
    private float _speed = 30f;

    private void Update()
    {
        if (isActiveAndEnabled)
            Move();
    }

    public void SetTargetPosition(Vector3 targetPosition, Vector3 targetPositionOffset)
    {
        _targetPosition = targetPosition + targetPositionOffset;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            gameObject.SetActive(false);
            if (collision.gameObject.TryGetComponent<UFO>(out UFO UFO))
            {
                OnUFODestroyed?.Invoke();
                OnObstacleDestroyed?.Invoke();
            }
                
            else if (collision.gameObject.TryGetComponent<Satellite>(out Satellite satellite))
                OnObstacleDestroyed?.Invoke();
            else if (collision.gameObject.TryGetComponent<OncomingRocket>(out OncomingRocket oncomingRocket))
                OnObstacleDestroyed?.Invoke();
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        transform.position = _defaultPosition;
    }
}
