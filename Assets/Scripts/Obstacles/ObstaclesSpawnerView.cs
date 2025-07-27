using System;
using System.Collections;
using UnityEngine;

public class ObstaclesSpawnerView : MonoBehaviour
{
    public event Action OnTimeToSpawnPattern;

    [SerializeField] private GameObject _asteroidPool;
    [SerializeField] private GameObject _satellitePool;
    [SerializeField] private GameObject _UFOPool;
    [SerializeField] private GameObject _oncomingRocketSpawner;

    private AsteroidPool asteroidPool;
    private SatellitePool satellitePool;
    private UFOPool UFOPool;
    private OncomingRocketSpawner oncomingRocketSpawner;

    private float _spawnDelay = 0.4f;
    private IEnumerator CachedSpawnAsteroidsRoutine;
    public void StartGame()
    {
        asteroidPool = _asteroidPool.GetComponent<AsteroidPool>();
        satellitePool = _satellitePool.GetComponent<SatellitePool>();
        UFOPool = _UFOPool.GetComponent<UFOPool>();
        oncomingRocketSpawner = _oncomingRocketSpawner.GetComponent<OncomingRocketSpawner>();
        CachedSpawnAsteroidsRoutine = SpawnAsteroidsRoutine();
        StartCoroutine(CachedSpawnAsteroidsRoutine);
    }

    public void SpawnObstacle(int obstacleIndex, float xCoordinate)
    {
        Vector3 ObstaclePosiiton = new(xCoordinate, 30f, -2f);

        switch (obstacleIndex)
        {
            case 1:
                asteroidPool.SpawnAsteroid(ObstaclePosiiton, xCoordinate);
                break;
            case 2:
                satellitePool.SpawnSatellite(ObstaclePosiiton, xCoordinate);
                break;
            case 3:
                UFOPool.SpawnUFO(ObstaclePosiiton, xCoordinate);
                break;
        }
    }

    private IEnumerator SpawnAsteroidsRoutine()
    {
        if (Time.timeScale >= 1)
        {
            while (isActiveAndEnabled)
            {
                var wait = new WaitForSeconds(_spawnDelay);
                yield return wait;
                OnTimeToSpawnPattern?.Invoke();
            }
        }
        else
            Debug.Log("Incorrect time scale");
    }

    private IEnumerator WaitForNextPatternRoutine()
    {
        if (Time.timeScale >= 1)
        {
            float delay = 1.2f;
            var wait = new WaitForSeconds(delay);
            for (int i = 0; i < 1; i++)
            {
                yield return wait;
                StartCoroutine(CachedSpawnAsteroidsRoutine);
            }
        }
        else
            Debug.Log("Incorrect time scale");
    }

    private IEnumerator SpawnOncomingRocketRoutine()
    {
        if (Time.timeScale >= 1)
        {
            float delay = 1.8f;
            var wait = new WaitForSeconds(delay);
            for (int i = 0; i < 1; i++)
            {
                Debug.Log("Rocket Spawning routine started");
                yield return wait;
                oncomingRocketSpawner.SpawnOncomingRocket();
                WaitForNextPattern();
            }
        }
        else
            Debug.Log("Incorrect time scale");
    }

    public void WaitForNextPattern()
    {
        StopCoroutine(CachedSpawnAsteroidsRoutine);
        StartCoroutine(WaitForNextPatternRoutine());
    }

    public void SpawnOncomingRocket()
    {
        StopCoroutine(CachedSpawnAsteroidsRoutine);
        StartCoroutine(SpawnOncomingRocketRoutine());
    } 

    public void ClearGameField()
    {
        asteroidPool.DeactivateAll();
        satellitePool.DeactivateAll();
        UFOPool.DeactivateAll();
        oncomingRocketSpawner.DeactivateRocket();
        StopAllCoroutines();
    }

    public void ClearGameFieldAndContinue()
    {
        asteroidPool.DeactivateAll();
        satellitePool.DeactivateAll();
        UFOPool.DeactivateAll();
        oncomingRocketSpawner.DeactivateRocket();
    }
}
