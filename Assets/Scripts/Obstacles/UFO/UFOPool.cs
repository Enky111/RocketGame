using System.Collections.Generic;
using UnityEngine;

public class UFOPool : MonoBehaviour
{
    [SerializeField] private UFO UFO;

    private int _poolSize = 1;
    private ObjectPool<UFO> _pool;
    private Vector3 _defaultPosition = new Vector3(0f, 30f, -2);
    private List<GameObject> _activeElements = new();
    private void Awake()
    {
        _pool = new ObjectPool<UFO>(UFO, _poolSize);
    }

    public GameObject SpawnUFO(Vector3 position, float xCoordinate)
    {
        var UFO = _pool.GetFreeElement();
        UFO.transform.position = position;
        UFO.SetTargetPosition(xCoordinate);
        UFO.OnObstacleToRemove += RemoveUFO;
        UFO.SetupUFO();
        _activeElements.Add(UFO.gameObject);
        UFO.ResetView();
        return UFO.gameObject;

    }

    public void RemoveUFO(GameObject UFO)
    {
        var UFOScript = UFO.GetComponent<UFO>();
        UFOScript.OnObstacleToRemove -= RemoveUFO;
        UFO.SetActive(false);
        UFO.transform.position = _defaultPosition;
    }

    public void DeactivateAll()
    {
        foreach (var element in _activeElements)
        {
            RemoveUFO(element);
        }
        _activeElements.Clear();
    }
}
