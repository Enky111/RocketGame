using System;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    protected float _speed = 15f;
    public event Action<GameObject> OnObstacleToRemove;
    private Vector3 _targetPosition;
    private float _gameSpeedMultiplier = 1f;

    private void Update()
    {
        if (isActiveAndEnabled)
            Move();
    }

    public void SetTargetPosition(float xCoordinate)
    {
        _targetPosition = new Vector3(xCoordinate, -20, -2);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, (_speed * _gameSpeedMultiplier) * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            OnObstacleToRemove?.Invoke(gameObject);
    }

    public void InvokeRemovingAction()
    {
        OnObstacleToRemove?.Invoke(gameObject);
    }
}
