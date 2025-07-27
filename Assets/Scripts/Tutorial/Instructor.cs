using System;
using System.IO;
using UnityEngine;

public class Instructor
{
    public event Action OnTutorialEnded;

    private UIChanger _UIChanger;

    private string _sourcePath = Path.Combine(Application.streamingAssetsPath, "tutorialStatus.json");
    private string _targetPath = Path.Combine(Application.persistentDataPath, "tutorialStatus.json");

    public void Setup(UIChanger UIChanger)
    {
        _UIChanger = UIChanger;

        if (!File.Exists(_targetPath))
        {
#if UNITY_ANDROID
                using (var www = UnityEngine.Networking.UnityWebRequest.Get(_sourcePath))
                {
                    www.SendWebRequest();
                    while (!www.isDone) { }
                    File.WriteAllBytes(_targetPath, www.downloadHandler.data);
                }
#else
                File.Copy(sourcePath, targetPath);
#endif
        }
    }
    public void CheckTutorialStatus()
    {
        if (LoadTutorialStatus() == false)
            StartTutorial();
    }

    public void CreateJson()
    {
        TutorialStatus tutorialStatus = new TutorialStatus();

        string json = JsonUtility.ToJson(tutorialStatus, true);
        File.WriteAllText(_targetPath, json);
    }

    private void PassTutorial()
    {
        TutorialStatus tutorialStatus = new TutorialStatus();
        tutorialStatus.isTutorialPassed = true;

        string json = JsonUtility.ToJson(tutorialStatus, true);
        File.WriteAllText(_targetPath, json);
    }

    public void StartTutorial()
    {
        _UIChanger.ActivateTutorialPanel();
        Debug.Log("tutorial");
        PassTutorial();
    }

    public void EndTutorial()
    {
        _UIChanger.DeactivateTutorialPanel();
        _UIChanger.ActivateTutorialButton();
        OnTutorialEnded?.Invoke();
    }

    private bool LoadTutorialStatus()
    {
        if (!File.Exists(_targetPath))
            CreateJson();
        string json = File.ReadAllText(_targetPath);
        TutorialStatus tutorialStatus = JsonUtility.FromJson<TutorialStatus>(json);
        return tutorialStatus.isTutorialPassed;
    }
}
