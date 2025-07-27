using UnityEngine;

public class MovingStars : MonoBehaviour
{
    private float _speed = 8f;
    private Vector3[] _waypoints = new Vector3[3];
    private Vector3 _targetWaypoint;
    private int _currentWaypointIndex = 0;
    private Vector3 _startPosition = new Vector3(0, 30f, -1f);
    private bool _isgameStarted = false;

    private void Start()
    {
        _waypoints[0] = new Vector3(0, 30f, -1f);
        _waypoints[1] = new Vector3(0, 0, -1f);
        _waypoints[2] = new Vector3(0, -30f, -1f);

        if (transform.position != _startPosition)
            _currentWaypointIndex = 1;
    }

    private void Update()
    {
        if (_isgameStarted)
            Move();
    }

    public void StartGame() => _isgameStarted = true;

    private void Move()
    {
        _targetWaypoint = _waypoints[_currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetWaypoint) < 0.1f && _currentWaypointIndex < _waypoints.Length - 1)
            _currentWaypointIndex++;
        else if (Vector3.Distance(transform.position, _targetWaypoint) < 0.1f && _currentWaypointIndex == _waypoints.Length - 1)
        {
            transform.position = _startPosition;
            _currentWaypointIndex = 0;
        }
            
    }
}

