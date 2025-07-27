using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Directory = System.IO.Directory;
using File = System.IO.File;

public class RecordSaver
{
    public event Action OnFilesCopied;
    private string _sourcePath = Path.Combine(Application.streamingAssetsPath, "scoreRecord.json");
    private string _targetPath = Path.Combine(Application.persistentDataPath, "scoreRecord.json");
    private string _directory;
    public void Setup()
    {
        _directory = Path.GetDirectoryName(_targetPath);
    }
    public IEnumerator CopyFile()
    {
        if (!File.Exists(_targetPath))
        {
#if UNITY_ANDROID
            using var www = UnityEngine.Networking.UnityWebRequest.Get(_sourcePath);
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    if (!Directory.Exists(_directory))
                        Directory.CreateDirectory(_directory);
                    File.WriteAllBytes(_targetPath, www.downloadHandler.data);
                    OnFilesCopied?.Invoke();
                }
                else
                    yield break;
            }
#else
                File.Copy(sourcePath, targetPath);
                OnFilesCopied?.Invoke();
#endif
        }
    }
    public void SaveRecord(int newRecord)
    {
        ScoreRecord scoreRecord = new ScoreRecord();
        scoreRecord._scoreRecord = newRecord;

        string json = JsonUtility.ToJson(scoreRecord, true);
        File.WriteAllText(_targetPath, json);
    }
    public int LoadRecord()
    {
        string json = File.ReadAllText(_targetPath);
        ScoreRecord scoreRecord = JsonUtility.FromJson<ScoreRecord>(json);
        return scoreRecord._scoreRecord;
    }
}
 