using UnityEngine;

public class OncomingRocketSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _oncomingRocketPrefab;
    private OncomingRocket _oncomingRocketScript;
    private GameObject _oncomingRocket;
    private float[] xCoordinates = { -4,5f, 0f, 4.5f};
    private float xCoordinate;
    private Vector3 _defaultPosition;

    private void Awake()
    {
        CreateOncomingRocket();
    }

    private void CreateOncomingRocket()
    {
        _oncomingRocket = Instantiate(_oncomingRocketPrefab);
        _oncomingRocket.SetActive(false);
        _oncomingRocketScript = _oncomingRocket.GetComponent<OncomingRocket>();
    }

    public void SpawnOncomingRocket()
    {
        _oncomingRocket.transform.position = ChooseRandomPosition();
        _oncomingRocketScript.SetTargetPosition(xCoordinate);
        _oncomingRocketScript.OnObstacleToRemove += RemoveOncomingRocket;
        _oncomingRocket.SetActive(true);
        _oncomingRocketScript.ResetView();
    }

    private void RemoveOncomingRocket(GameObject oncomingRocket)
    {
        var oncomingRocketScript = oncomingRocket.GetComponent<OncomingRocket>();
        oncomingRocketScript.OnObstacleToRemove -= RemoveOncomingRocket;
        oncomingRocket.SetActive(false);
    }

    private Vector3 ChooseRandomPosition()
    {
        int randomXCoordinateIndex = Random.Range(0, 3);
        _defaultPosition = new(xCoordinates[randomXCoordinateIndex], 30f, -2f);
        xCoordinate = xCoordinates[randomXCoordinateIndex];
        return _defaultPosition;
    }

    public void DeactivateRocket() => RemoveOncomingRocket(_oncomingRocket);
}
