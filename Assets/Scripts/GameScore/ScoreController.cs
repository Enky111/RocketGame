using System;

public class ScoreController
{
    public event Action OnScoreMultiplierIncreased;
    private ScoreModel _scoreModel;
    private ScoreView _scoreView;
    private bool _isGameStarted = false;

    public ScoreController(ScoreModel model, ScoreView view)
    {
        _scoreModel = model;
        _scoreView = view;

        _scoreModel.OnScoreMultiplierIncreased += ScoreMultiplierIncrease;
    }

    public void ScoreMultiplierIncrease() => OnScoreMultiplierIncreased?.Invoke();

    public void TransferScore()
    {
        float score = _scoreModel.ScoreUpdating();
        _scoreView.GetScore(score);
    }

    public int GetScore()
    {
        int score = _scoreView._scoreToDisplay;
        return score;
    }

    public void StartGame()
    {
        _isGameStarted = true;
        _scoreView.StartGame(_isGameStarted);
    }

    public void EndGame() 
    {
        _scoreView.EndGame();
        ResetScore();
    } 

    public void ResetScore()
    {
        _scoreModel.ResetScore();
        _scoreView.ResetScore();
    }
}
