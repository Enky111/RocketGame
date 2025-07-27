using System.Collections.Generic;
using UnityEngine;

public class SatellitePool : MonoBehaviour
{
    [SerializeField] private Satellite _satellite;

    private ObjectPool<Satellite> _pool;
    private int _poolSize = 2;
    private Vector3 _defaultPosition = new Vector3(0f, 30f, -2);
    private List<GameObject> _activeElements = new();
    private void Awake()
    {
        _pool = new ObjectPool<Satellite>(_satellite, _poolSize);
    }

    public GameObject SpawnSatellite(Vector3 position, float xCoordinate)
    {
        var satellite = _pool.GetFreeElement();
        satellite.transform.position = position;
        satellite.SetTargetPosition(xCoordinate);
        satellite.OnObstacleToRemove += RemoveSatellite;
        _activeElements.Add(satellite.gameObject);
        satellite.ResetView();
        return satellite.gameObject;
    }

    public void RemoveSatellite(GameObject satellite)
    {
        var satelliteScript = satellite.GetComponent<Satellite>();
        satelliteScript.OnObstacleToRemove -= RemoveSatellite;
        satellite.SetActive(false);
        satellite.transform.position = _defaultPosition;
    }

    public void DeactivateAll()
    {
        foreach (var element in _activeElements)
        {
            RemoveSatellite(element);
        }
        _activeElements.Clear();
    }
}
