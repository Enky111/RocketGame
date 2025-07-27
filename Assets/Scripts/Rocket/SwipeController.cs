using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    public event Action OnSwipedLeft;
    public event Action OnSwipedRight;
    public event Action OnTaped;

    private bool _isTouchOnUI = false;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private bool _isGameStarted = false;
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            _startTouchPosition = touch.position;

#if UNITY_ANDROID
            _isTouchOnUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
#else
                _touchStartedOverUI = EventSystem.current.IsPointerOverGameObject();
#endif
            _startTouchPosition = touch.position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Touch touch = Input.GetTouch(0);
            _endTouchPosition = touch.position;

            float XPositionDefference  = _endTouchPosition.x - _startTouchPosition.x;

            if (_endTouchPosition.x > _startTouchPosition.x && Math.Abs(XPositionDefference) > 5f)
                MoveRight();
            if (_endTouchPosition.x < _startTouchPosition.x && Math.Abs(XPositionDefference) > 5f)
                MoveLeft();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && _isGameStarted == true)
        {
            Touch touch = Input.GetTouch(0);
            _endTouchPosition = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                _endTouchPosition = touch.position;

                if (_endTouchPosition.x == _startTouchPosition.x)
                {
                    if (_isTouchOnUI)
                        return;

                    Attack();
                }
            }
        }
    }

    private void MoveRight()
    {
        OnSwipedRight?.Invoke();
        Debug.Log("swiped to right");
    } 

    private void MoveLeft()
    {
        OnSwipedLeft?.Invoke();
        Debug.Log("swiped to left");
    }

    private void Attack()
    {
        OnTaped?.Invoke();
    }

    public void StartGame() => _isGameStarted = true;
    public void EndGame() => _isGameStarted = false;
}
