using UnityEngine;

public class RocketModel
{
    private Vector3[] _positions;
    public float Speed = 25f;
    public int _currentPositionIndex = 1;
    public float rotationAngle = 15f;
    private bool _isForceFieldActive = false;

    public RocketModel(Vector3[] positions)
    {
        _positions = positions;
    }
    public Vector3 MoveLeft()
    {
        if (_currentPositionIndex > 0)
            _currentPositionIndex--;
        Debug.Log("moved left");
        return _positions[_currentPositionIndex];
    }

    public Vector3 MoveRight()
    {
        if (_currentPositionIndex < 2)
            _currentPositionIndex++;
        Debug.Log("moved right");
        return _positions[_currentPositionIndex];
    }

    public Vector3 Attack()
    {
        Debug.Log("Attcked");
        return _positions[_currentPositionIndex];
    }

    public Vector3 ResetPosition()
    {
        _currentPositionIndex = 1;
        return _positions[_currentPositionIndex];
    }

    public void ActivateForceField() 
    {
        if (_isForceFieldActive)
            return;
        _isForceFieldActive = true;
    } 

    public bool RocketCollided(bool isKilledByUFO)
    {
        if (_isForceFieldActive && !isKilledByUFO)
        {
            _isForceFieldActive = false;
            return false;
        }
        return true;
    }
}
