using System.Collections.Generic;
using UnityEngine;


public class AsteroidPool : MonoBehaviour
{
    [SerializeField] private AsteroidMoving _asteroidPrefab1;
    [SerializeField] private AsteroidMoving _asteroidPrefab2;

    private int _poolSize = 5;
    private ObjectPool<AsteroidMoving> _pool1;
    private ObjectPool<AsteroidMoving> _pool2;
    private Vector3 _defaultPosition = new Vector3(0f, 30f, -2);
    private List<GameObject> _activeElements = new();
    private AsteroidMoving _asteroid;

    private void Awake()
    {
        _pool1 = new ObjectPool<AsteroidMoving>(_asteroidPrefab1, _poolSize);
        _pool2 = new ObjectPool<AsteroidMoving>(_asteroidPrefab2, _poolSize);     
    }

    public GameObject SpawnAsteroid(Vector3 position, float xCoordinate)
    {
        int randomAsteroid = Random.Range(0, 2);
        switch (randomAsteroid)
        {
            case 0:
                _asteroid = _pool1.GetFreeElement();
                break;
            case 1:
                _asteroid = _pool2.GetFreeElement();
                break;
        }
        if (_asteroid)
        {
            _asteroid.gameObject.transform.position = position;
            _asteroid.SetTargetPosition(xCoordinate);
            _asteroid.OnObstacleToRemove += RemoveAsteroid;
            _activeElements.Add(_asteroid.gameObject);
            return _asteroid.gameObject;
        }
        Debug.Log("asteroid is null");
        return null;
    }

    public void RemoveAsteroid(GameObject asteroid)
    {
        var asteroidScript = asteroid.GetComponent<AsteroidMoving>();
        asteroidScript.OnObstacleToRemove -= RemoveAsteroid;
        asteroid.SetActive(false);
        asteroid.transform.position = _defaultPosition;
    }

    public void DeactivateAll()
    {
        foreach (var element in _activeElements)
        {
            RemoveAsteroid(element);
        }
        _activeElements.Clear();
    }
}
