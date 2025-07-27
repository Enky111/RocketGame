using TMPro;
using UnityEngine;


public class UIChanger : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _looseMenu;
    [SerializeField] private GameObject _tutorialButton;
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _tutorialButtonNext;
    [SerializeField] private GameObject _tutorialButtonPrevious;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _smallSettingsButton;

    [SerializeField] private TextMeshProUGUI _looseMenuScoreCountText;
    [SerializeField] private TextMeshProUGUI _looseMenuScoreText;

    [SerializeField] private GameObject[] _tutorialText;

    private int _currentTutorialText = 0;

    public void ActivateStartButton() => _startButton.SetActive(true);

    public void DeactivateStartButton() => _startButton.SetActive(false);

    public void ActivateTutorialButton() => _tutorialButton.SetActive(true);

    public void DeactivateTutorialButton() => _tutorialButton.SetActive(false);

    public void ActivateLooseMenu() => _looseMenu.SetActive(true);

    public void DeactivateLooseMenu() => _looseMenu.SetActive(false);

    public void ShowLooseMenu(int ScoreCount)
    {
        _looseMenuScoreCountText.text = $"{ScoreCount}";
        _looseMenu.SetActive(true);
    }

    public void ActivatePauseMenu() => _pauseMenu.SetActive(true);

    public void DeactivatePauseMenu() => _pauseMenu.SetActive(false);

    public void SetLooseMenuText(string Text) 
    {
        _looseMenuScoreText.text = Text;
    }

    public void DeactivateTutorialPanel()
    {
        _tutorialText[_currentTutorialText].SetActive(false);
        _tutorialPanel.SetActive(false);
    }

    private void ActivateTutorialButtonNext() => _tutorialButtonNext.SetActive(true);

    private void DeactivateTutorialButtonNext() => _tutorialButtonNext.SetActive(false);

    private void ActivateTutorialButtonPrevious() => _tutorialButtonPrevious.SetActive(true);

    private void DeactivateTutorialButtonPrevious() => _tutorialButtonPrevious.SetActive(false);

    public void ActivatePauseButton() => _pauseButton.SetActive(true);

    public void DeactivatePauseButton() => _pauseButton.SetActive(false);

    public void DeactivateSmallSettingsButton() => _smallSettingsButton.SetActive(false);

    public void ActivateSmallSettingsButton() => _smallSettingsButton.SetActive(true);

    public void ActivateSettingsWindow() => _settingsWindow.SetActive(true);

    public void DeactivateSettingsWindow() => _settingsWindow.SetActive(false);

    public void BackToPauseMenu()
    {
        _settingsWindow.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void ActivateTutorialPanel()
    {
        _currentTutorialText = 0;
        DeactivateTutorialButtonPrevious();
        ActivateTutorialButtonNext();
        _tutorialText[_currentTutorialText].SetActive(true);
        _tutorialPanel.SetActive(true);
    }

    public void SwipeTextToNext()
    {
        _tutorialText[_currentTutorialText].SetActive(false);
        _currentTutorialText++;
        _tutorialText[_currentTutorialText].SetActive(true);

        if (_currentTutorialText > 0)
            ActivateTutorialButtonPrevious();

        if (_currentTutorialText == _tutorialText.Length - 1)
            DeactivateTutorialButtonNext();
    }

    public void SwipeTextToPrevious()
    {
        _tutorialText[_currentTutorialText].SetActive(false);
        _currentTutorialText--;
        _tutorialText[_currentTutorialText].SetActive(true);

        if (_currentTutorialText == 0)
            DeactivateTutorialButtonPrevious();

        if (_currentTutorialText < _tutorialText.Length - 1)
            ActivateTutorialButtonNext();
    }
}


