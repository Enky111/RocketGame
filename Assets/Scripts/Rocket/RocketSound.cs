using System;
using UnityEngine;

[Serializable]
public struct RocketSound
{
    [SerializeField] private AudioClip _clip;

    public AudioClip Clip => _clip;
}
