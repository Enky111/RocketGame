using UnityEngine;
using UnityEngine.UI;

public class BackgroundSound : MonoBehaviour
{
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;

    [SerializeField] private GameObject _musicVolumeSlider;

    private AudioSource _audioSource;
    private Slider MusicVolumeSlider;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        MusicVolumeSlider = _musicVolumeSlider.GetComponent<Slider>();
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        PlayMusic(true);
    }

    private void Update()
    {
        _audioSource.volume = MusicVolumeSlider.value;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
        PlayerPrefs.Save();
    }
    public void PlayMusic(bool isInMenu)
    {
        _audioSource.Stop();
        if (isInMenu)
            _audioSource.clip = _menuMusic;
        else
            _audioSource.clip = _gameMusic;
        _audioSource.Play();
    }
}
