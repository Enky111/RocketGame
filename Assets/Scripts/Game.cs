using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _rocket;
    [SerializeField] private GameObject _leftPoint;
    [SerializeField] private GameObject _centralPoint;
    [SerializeField] private GameObject _rightPoint;
    [SerializeField] private GameObject _starsOnScreen;
    [SerializeField] private GameObject _starsOverScreen;
    [SerializeField] private GameObject _scoreDisplay;
    [SerializeField] private GameObject _obstaclesSpawner;
    [SerializeField] private GameObject _backgroundSound;
    [SerializeField] private GameObject _UIChanger;
    [SerializeField] private GameObject _sounds;
    [SerializeField] private GameObject _swipeController;

    private RocketView _rocketView;
    private RocketModel _rocketModel;
    private RocketController _rocketController;
    private MovingStars _movingOnScreenStars;
    private MovingStars _movingOverScreenStars;
    private ObstaclesSpawnerView _obstaclesSpawnerView;
    private ObstaclesSpawnerController _obstaclesSpawnerController;
    private ObstacleSpawnerModel _obstacleSpawnerModel;
    private ScoreModel _scoreModel;
    private ScoreController _scoreController;
    private ScoreView _scoreView;
    private RecordSaver _recordSaver;
    private BackgroundSound BackgroundSound;
    private Instructor _instructor;
    private UIChanger UIChanger;
    private Sounds Sounds;
    private SwipeController swipeController;

    private bool _isPaused = false;
    private bool _isInMenu = true;
    private int _scoreCount;
    private int _record;
    private float _gameSpeedMultiplier = 1.1f;
    private float _currentTimeScale;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        Time.timeScale = 0f;
        _currentTimeScale = Time.timeScale;

        Vector3[] positions =
        {
            _leftPoint.transform.position,
            _centralPoint.transform.position,
            _rightPoint.transform.position,
        };
        _rocketView = _rocket.GetComponent<RocketView>();
        _rocketView.Setup();
        _rocketModel = new(positions);
        _rocketController = new(_rocketModel, _rocketView);

        _movingOnScreenStars = _starsOnScreen.GetComponent<MovingStars>();
        _movingOverScreenStars = _starsOverScreen.GetComponent<MovingStars>();
        _obstaclesSpawnerView = _obstaclesSpawner.GetComponent<ObstaclesSpawnerView>();
        _obstacleSpawnerModel = new();
        _obstaclesSpawnerController = new(_obstaclesSpawnerView, _obstacleSpawnerModel);

        _scoreView = _scoreDisplay.GetComponent<ScoreView>();
        _scoreModel = new();
        _scoreController = new(_scoreModel, _scoreView);

        BackgroundSound = _backgroundSound.GetComponent<BackgroundSound>();

        UIChanger = _UIChanger.GetComponent<UIChanger>();
        Sounds = _sounds.GetComponent<Sounds>();
        swipeController = _swipeController.GetComponent<SwipeController>();

        _instructor = new();
        _instructor.Setup(UIChanger);
        _instructor.CheckTutorialStatus();

        _recordSaver = new();
        _recordSaver.Setup();
        StartCoroutine(_recordSaver.CopyFile());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPaused == false){
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused == true)
        {
            Resume();
        }
    }

    private void OnEnable()
    {
        _rocketController.OnCollidedWithProtection += ClearGameFieldAndContinue;
        _rocketController.OnGameOver += EndGame;
        _instructor.OnTutorialEnded += ShowStartButton;
        _recordSaver.OnFilesCopied += LoadRecord;
    }

    private void OnDisable()
    {
        _rocketController.OnCollidedWithProtection -= ClearGameFieldAndContinue;
        _rocketController.OnGameOver -= EndGame;
        _instructor.OnTutorialEnded -= ShowStartButton;
        _recordSaver.OnFilesCopied -= LoadRecord;
    }

    private void FixedUpdate()
    {
        _scoreController.TransferScore();
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
        _currentTimeScale = Time.timeScale;
        _movingOnScreenStars.StartGame();
        _movingOverScreenStars.StartGame();
        _scoreController.StartGame();
        _scoreController.OnScoreMultiplierIncreased += IncreaseGameSpeed;
        _obstaclesSpawnerController.StartGame();
        _rocketView.StartGame();
        UIChanger.DeactivateStartButton();
        UIChanger.DeactivateTutorialButton();
        UIChanger.ActivatePauseButton();
        UIChanger.DeactivateSmallSettingsButton();
        BackgroundSound.PlayMusic(false);
        swipeController.StartGame();
        _isInMenu = false;
        TransferAnimData();
    }

    public void RestartGame()
    {
        UIChanger.DeactivatePauseMenu();
        _obstaclesSpawnerController.ClearGameField();
        _obstaclesSpawnerController.RestartGame();
        _rocketController.RestartGame();
        _scoreController.ResetScore();
        StartGame();
        _rocketView.BeginAnimation();
    }
    private void StopTime() => Time.timeScale = 0;

    private void StartTime() => Time.timeScale = _currentTimeScale;

    public void Pause()
    {
        if(_isInMenu)
            return;
        StopTime();
        _isPaused = true;
        UIChanger.ActivatePauseMenu();
        swipeController.EndGame();
        TransferAnimData();
    }

    public void Resume()
    {
        StartTime();
        _isPaused = false;
        UIChanger.DeactivatePauseMenu();
        swipeController.StartGame();
        TransferAnimData();
    }

    private void ShowLooseMenu()
    {
        _scoreCount = _scoreController.GetScore();
        WriteRecord();
        UIChanger.ShowLooseMenu(_scoreCount);
    }

    private void ShowStartButton() => UIChanger.ActivateStartButton();

    public void ExitToStartScreen()
    {
        UIChanger.DeactivateLooseMenu();
        UIChanger.ActivateStartButton();
        _rocketController.RestartGame();
        UIChanger.ActivateTutorialButton();
        UIChanger.DeactivatePauseButton();
        UIChanger.DeactivatePauseMenu();
        UIChanger.ActivateSmallSettingsButton();
        _obstaclesSpawnerController.ClearGameField();
        _scoreController.EndGame();
        BackgroundSound.PlayMusic(true);
        TransferAnimData();
        _isInMenu=true;
    }

    private void ClearGameFieldAndContinue() => _obstaclesSpawnerController.ClearGameFieldAndContinue();

    public void CloseSettingsWindow()
    {
        if (_isInMenu == false)
        {
            UIChanger.DeactivateSettingsWindow();
            UIChanger.BackToPauseMenu();
            Sounds.SaveSettings();
            BackgroundSound.SaveSettings();
        }
        else
        {
            UIChanger.DeactivateSettingsWindow();
            UIChanger.ActivateStartButton();
            UIChanger.ActivateSmallSettingsButton();
            Sounds.SaveSettings();
            BackgroundSound.SaveSettings();
        }
    }

    public void OpenSettingsWindow()
    {
        if (_isInMenu == false)
        {
            UIChanger.DeactivatePauseMenu();
            UIChanger.ActivateSettingsWindow();
        }
        else
        {
            UIChanger.DeactivateSmallSettingsButton();
            UIChanger.DeactivateStartButton();
            UIChanger.ActivateSettingsWindow();
        }
    }

    private void WriteRecord() 
    {
        if (_scoreCount > _record)
        {
            _recordSaver.SaveRecord(_scoreCount);
            _record = _recordSaver.LoadRecord();
            UIChanger.SetLooseMenuText("NEW RECORD");
        }
        else
            UIChanger.SetLooseMenuText("YOUR SCORE");
    } 
    private void EndGame()
    {
        StopTime();
        ShowLooseMenu();
        _obstaclesSpawnerController.ClearGameField();
        _obstaclesSpawnerController.RestartGame();
        BackgroundSound.PlayMusic(true);
        swipeController.EndGame();
    }

    private void IncreaseGameSpeed()
    {
        Time.timeScale *= _gameSpeedMultiplier;
        _currentTimeScale = Time.timeScale;
        Debug.Log("Time scale increased");
        Debug.Log(Time.timeScale);
    }

    public void StartTutorialAgain() 
    {
        _instructor.StartTutorial();
        UIChanger.DeactivateStartButton();
        UIChanger.DeactivateTutorialButton();
    } 

    private void TransferAnimData()
    {
        if (_isPaused == false)
            _rocketView.BeginAnimation();
        else
            _rocketView.StopAnimation();
    }

    private void LoadRecord() => _recordSaver.LoadRecord();
    public void EndTutorial() => _instructor.EndTutorial();
}
