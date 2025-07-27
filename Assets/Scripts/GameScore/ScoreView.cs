using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public event Action OnScoreToUpdate;

    [SerializeField] private TextMeshProUGUI _text;

    public int _scoreToDisplay;

    private bool _isgameStarted = false;
    private void FixedUpdate()
    {
        OnScoreToUpdate?.Invoke();
        if (_isgameStarted)
        {
            _text.text = $"{_scoreToDisplay}";
        }
    }

    public void StartGame(bool isGameStarted)
    {
        _isgameStarted = isGameStarted;
        _text.alpha = 1f;
    }

    public void EndGame() => _text.alpha = 0f;
    public void ResetScore()
    {
        _scoreToDisplay = 0;
        _text.text = $"{_scoreToDisplay}";
    }

    public int GetScore(float score)
    {
        _scoreToDisplay = (int)score;
        return _scoreToDisplay;
    }
}
