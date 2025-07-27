
public class ObstaclesSpawnerController
{
    private ObstaclesSpawnerView _view;
    private ObstacleSpawnerModel _model;

    public ObstaclesSpawnerController(ObstaclesSpawnerView view, ObstacleSpawnerModel model)
    {
        _view = view;
        _model = model;

        _view.OnTimeToSpawnPattern += SpawnPattern;
        _model.OnPatternSpawned += TransferPatternData;
        _model.OnPatternChanged += ChangeSpawnDelay;
        _model.OnTimeToSpawnOncomingRocket += SpawnOncomingRocket;
    }
    private void ChangeSpawnDelay() => _view.WaitForNextPattern();
    private void SpawnPattern() => _model.SpawnLine();
    private void TransferPatternData(int obstacleIndex, float xCoordinate)
    {
        _view.SpawnObstacle(obstacleIndex, xCoordinate);
    }

    public void SpawnOncomingRocket() => _view.SpawnOncomingRocket();

    public void StartGame() => _view.StartGame();
    public void RestartGame() => _model.RestartGame();
    public void ClearGameField() => _view.ClearGameField();

    public void ClearGameFieldAndContinue() => _view.ClearGameFieldAndContinue();

}
