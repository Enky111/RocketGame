using System;
using UnityEngine;

public class GameSpeedChanger : MonoBehaviour
{
    public event Action<float> OnMultiplierIncreased;
    private float _increaseValue = 1.1f;

    public void IncreaseMultilier() => OnMultiplierIncreased?.Invoke(_increaseValue);
}
