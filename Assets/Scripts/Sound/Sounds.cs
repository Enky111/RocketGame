using UnityEngine;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    [SerializeField] private RocketView _rocket;
    [SerializeField] private GameObject _soundsVolumeSlider;
    [SerializeField] private AudioClip _explosionSound;

    private AudioSource _soundsAudioSource;
    private AudioSource _rocketSounds;
    private Slider SoundsVolumeSlider;
    private Attack _rocketAttack;

    private void Start()
    {
        _soundsAudioSource = GetComponent<AudioSource>();
        _rocketSounds = _rocket.GetComponent<AudioSource>();
        SoundsVolumeSlider = _soundsVolumeSlider.GetComponent<Slider>();
        SoundsVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        _soundsAudioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
        _rocketSounds.volume = PlayerPrefs.GetFloat("SoundVolume");
        _rocketAttack = _rocket.attack;
        _rocketAttack.OnObstacleDestroyed += PlayExplosionSound;
    }

    public void SaveSettings()
    {
        _soundsAudioSource.volume = SoundsVolumeSlider.value;
        _rocketSounds.volume = SoundsVolumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", SoundsVolumeSlider.value);
        PlayerPrefs.Save();
    }

    private void PlayExplosionSound() => _soundsAudioSource.PlayOneShot(_explosionSound);
}
