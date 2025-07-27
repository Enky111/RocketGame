using System;
using UnityEngine;

public class RocketController
{
    public event Action OnGameOver;
    public event Action OnCollidedWithProtection;

    private RocketModel _rocketModel;
    private RocketView _rocketView;
    private Attack _attack;

    public RocketController(RocketModel model, RocketView view)
    {
        _rocketModel = model;
        _rocketView = view;

        _rocketView.OnMoveLeft += MoveLeft;
        _rocketView.OnMoveRight += MoveRight;
        _rocketView.OnAttackButtonPressed += Attack;
        _rocketView.OnRocketCollided += RocketCollided;
    }

    public void MoveLeft()
    {
        if (_rocketModel._currentPositionIndex > 0)
        {
            Vector3 targetPosition = _rocketModel.MoveLeft();
            _rocketView.Move(targetPosition, _rocketModel.Speed, _rocketModel.rotationAngle);
        }
    }

    public void MoveRight()
    {
        if (_rocketModel._currentPositionIndex < 2)
        {
            Vector3 targetPosition = _rocketModel.MoveRight();
            _rocketView.Move(targetPosition, _rocketModel.Speed, _rocketModel.rotationAngle * -1);
        }
    }

    private void Attack()
    {
        Vector3 attackPosition = _rocketModel.Attack();
        _rocketView.Attack(attackPosition);
        _attack = _rocketView.attack;
        _attack.OnUFODestroyed += ActivateForceField;
    }

    public void RestartGame()
    {
        Vector3 defaultPosition = _rocketModel.ResetPosition();
        _rocketView.RestartGame(defaultPosition);
    }
    private void ActivateForceField()
    {
        _rocketModel.ActivateForceField();
        _rocketView.ActivateForceField();

    }

    private void RocketCollided(bool isKilledByUFO)
    {
        bool isGameOver;
        isGameOver = _rocketModel.RocketCollided(isKilledByUFO);
        if (isGameOver)
        {
            _rocketView.SetCollisionSound(isGameOver);
            OnGameOver?.Invoke();
        }
        else
        {
            OnCollidedWithProtection?.Invoke();
            _rocketView.DeactivateForceField();
        }
            
    }
}
