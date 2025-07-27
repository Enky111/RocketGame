using System;
using UnityEngine;

public class ScoreModel
{
    public event Action OnScoreMultiplierIncreased;
    private float _score = 0;
    private float _scoreMultiplier = 50f;
    private float _nextScoreThreshold = 2000f;

    public void ResetScore()
    {
        _score = 0;
        _scoreMultiplier = 50f;
    }

    public float ScoreUpdating()
    {
        _score += _scoreMultiplier * Time.deltaTime;

        if (_score >= _nextScoreThreshold)
        {
            _scoreMultiplier *= 1.1f;
            OnScoreMultiplierIncreased?.Invoke();
            _nextScoreThreshold += 2000f;
        }
            
        return _score;
    }
}
