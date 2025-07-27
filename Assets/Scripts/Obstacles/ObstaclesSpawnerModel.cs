using System;
using UnityEngine;

public class ObstacleSpawnerModel
{
    public event Action<int, float> OnPatternSpawned;
    public event Action OnPatternChanged;
    public event Action OnTimeToSpawnOncomingRocket;
    private int[,] _asteroidPattern = null;
    private int _patternLine = 0;
    private int _patternLength;
    private int _spawnedPatternsCount = 0;
    private int _randomPatternCount = UnityEngine.Random.Range(7, 11);

    private float[] _xPositions = { -4.5f, 0f, 4.5f };

    private int[,] _asteroidPattern1 = new int[5, 3]
    {
        {0, 0, 1 },
        {1, 0, 1 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 3, 0 }
    };

    private int[,] _asteroidPattern2 = new int[5, 3]
    {
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 1 }
    };

    private int[,] _asteroidPattern3 = new int[8, 3]
    {
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {2, 0, 0 },
        {0, 0, 1 },
        {0, 0, 1 },
        {0, 1, 0 },
    };

    private int[,] _asteroidPattern4 = new int[8, 3]
    {
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 2 },
        {1, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
    };

    private int[,] _asteroidPattern5 = new int[6, 3]
    {
        {0, 0, 1 },
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 1, 0 },
    };

    private int[,] _asteroidPattern6 = new int[6, 3]
    {
        {1, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 1 },
    };

    private int[,] _asteroidPattern7 = new int[13, 3]
    {
        {0, 0, 1 },
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 1 },
    };

    private int[,] _asteroidPattern8 = new int[13, 3]
    {
        {1, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {1, 0, 0 },
    };

    private int[,] _asteroidPattern9 = new int[16, 3]
    {
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {2, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
    };

    private int[,] _asteroidPattern10 = new int[16, 3]
    {
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 2 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
    };

    private int[,] _asteroidPattern11 = new int[16, 3]
    {
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 3, 0 },
        {0, 0, 2 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
    };

    private int[,] _asteroidPattern12 = new int[20, 3]
    {
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 0, 0 },
        {1, 0, 0 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 2, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 3 },
    };

    private int[,] _asteroidPattern13 = new int[20, 3]
    {
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 2, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {3, 0, 0 },
    };

    private int[,] _asteroidPattern14 = new int[17, 3]
    {
        {1, 0, 0 },
        {1, 0, 1 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {2, 0, 0 },
        {0, 3, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 1 },
        {1, 0, 1 },
    };

    private int[,] _asteroidPattern15 = new int[17, 3]
    {
        {0, 0, 1 },
        {1, 0, 1 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 2 },
        {0, 3, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {1, 0, 0 },
        {1, 0, 1 },
    };

    private int[,] _asteroidPattern16 = new int[20, 3]
    {
        {0, 1, 0 },
        {0, 0, 0 },
        {2, 0, 0 },
        {0, 1, 0 },
        {1, 0, 0 },
        {0, 0, 1 },
        {0, 0, 1 },
        {1, 0, 0 },
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 3, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 2 },
    };

    private int[,] _asteroidPattern17 = new int[20, 3]
    {
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 2 },
        {0, 1, 0 },
        {0, 0, 1 },
        {1, 0, 0 },
        {1, 0, 0 },
        {0, 0, 1 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 3, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {2, 0, 0 },
    };

    private int[,] _asteroidPattern18 = new int[19, 3]
    {
        {1, 0, 1 },
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 2, 0 },
        {0, 1, 0 },
        {0, 0, 2 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 3 },
        {0, 2, 0 },
    };

    private int[,] _asteroidPattern19 = new int[19, 3]
    {
        {1, 0, 1 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 2, 0 },
        {0, 1, 0 },
        {2, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {3, 0, 0 },
        {0, 2, 0 },
    };

    private int[,] _asteroidPattern20 = new int[20, 3]
    {
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 3, 0 },
        {0, 0, 2 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 3, 0 },
        {2, 0, 0 },
    };

    private int[,] _asteroidPattern21 = new int[20, 3]
    {
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 0 },
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 1, 0 },
        {0, 0, 0 },
        {0, 0, 0 },
        {1, 0, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 3, 0 },
        {2, 0, 0 },
        {0, 0, 0 },
        {0, 0, 1 },
        {0, 0, 0 },
        {0, 0, 0 },
        {0, 3, 0 },
        {0, 0, 2 },
    };

    private int[,] ChoosePatternToSpawn()
    {
        int randomPatternNumber = UnityEngine.Random.Range(1, 21);

        Debug.Log($"Pattern is {randomPatternNumber}");
        switch (randomPatternNumber)
        {
            case 1:
                return _asteroidPattern1;
            case 2:
                return _asteroidPattern2;
            case 3:
                return _asteroidPattern3;
            case 4:
                return _asteroidPattern4;
            case 5:
                return _asteroidPattern5;
            case 6:
                return _asteroidPattern6;
            case 7:
                return _asteroidPattern7;
            case 8:
                return _asteroidPattern8;
            case 9:
                return _asteroidPattern9;
            case 10:
                return _asteroidPattern10;
            case 11:
                return _asteroidPattern11;
            case 12:
                return _asteroidPattern12;
            case 13:
                return _asteroidPattern13;
            case 14:
                return _asteroidPattern14;
            case 15:
                return _asteroidPattern15;
            case 16:
                return _asteroidPattern16;
            case 17:
                return _asteroidPattern17;
            case 18:
                return _asteroidPattern18;
            case 19:
                return _asteroidPattern19;
            case 20:
                return _asteroidPattern20;
            case 21:
                return _asteroidPattern21;
        }
        return null;
    }

    public void SpawnLine()
    {
        
        if (_asteroidPattern == null)
        {
            _asteroidPattern = ChoosePatternToSpawn();

            if (_spawnedPatternsCount == _randomPatternCount)
            {
                OnTimeToSpawnOncomingRocket?.Invoke();
                Debug.Log("Rocket to Spawn");
                _spawnedPatternsCount = 0;
                _randomPatternCount = UnityEngine.Random.Range(5, 8);
            }
        }

        _patternLength = _asteroidPattern.GetLength(0);

        for (int i = _patternLine; i < _asteroidPattern.GetLength(0);)
        {
            for (int j = 0; j < _asteroidPattern.GetLength(1); j++)
            {
                if (_asteroidPattern[i, j] > 0)
                {
                    OnPatternSpawned?.Invoke(_asteroidPattern[i, j], _xPositions[j]);
                }
            }
            _patternLine++;
            
            if (_patternLine == _patternLength)
            {
                _asteroidPattern = null;
                _patternLine = 0;
                _spawnedPatternsCount++;
                OnPatternChanged?.Invoke();
                return;
            }
            break;
        }
    }

    public void RestartGame() => _spawnedPatternsCount = 0;
}