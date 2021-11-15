using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public string playerName = "Player";
    public Color playerColor = Color.white;
    public Color[] colorSet;

    string filePath = "";
    string saveFileNameExtension = ".sav";

    SaveData curSaveFile = null;

    private void Awake()
    {
        filePath = Application.dataPath + "/Saves";
    }

    public void GameStart()
    {
        UIStackManager.Clear();
        SceneManager.LoadScene("MainScene");
    }

    public void StartNewGame(string playerName, Color playerColor)
    {
        this.playerColor = playerColor;
        this.playerName = playerName;
        GameStart();
    }

    public void StartLoadedGame(SaveData data)
    {
        curSaveFile = data; 

        GameStart();
    }

    public void SaveData()
    {
        string dataString = JsonUtility.ToJson(
            new SaveData(TileMapData.Instance.GetAllTilesData(), 
            MainSceneManager.Instance.GetPlayer().PlayerData, 
            AIManager.Instance.GetAIDatas(), 
            MainSceneManager.Instance.turnCnt, 
            MainSceneManager.Instance.stageCount,
            MainSceneManager.Instance.mapSize, 
            MainSceneManager.Instance.isRerolled));

        string saveFileName = MainSceneManager.Instance.GetPlayer().MyName + "_" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + saveFileNameExtension;
        string saveFilePath = filePath + "/" ;

        if (!Directory.Exists(saveFilePath))
        {
            Directory.CreateDirectory(saveFilePath);
        }

        Debug.Log(saveFilePath + saveFileName);
        using (StreamWriter sw = File.CreateText(saveFilePath + saveFileName))
        {
            sw.Write(dataString);
        }
    }

    public void DeleteFile(SaveData save)
    {
        DirectoryInfo di = new DirectoryInfo(filePath);

        var selectedFiles = from item in di.GetFiles()
                            where item.Extension == saveFileNameExtension
                            select item;

        selectedFiles.ToList().Find(x => x.Name.Contains(save.saveTime.ToString("yyyy_MM_dd_HH_mm_ss"))).Delete();
    }

    public SaveData LoadData()
    {
        return curSaveFile;
    }

    public SaveData FileToData(FileInfo file)
    {
        return JsonUtility.FromJson<SaveData>(file.OpenText().ReadToEnd());
    }

    public FileInfo[] GetAllSaveFiles()
    {
        if (Directory.Exists(filePath))
        {
            DirectoryInfo di = new DirectoryInfo(filePath);

            var selectedFiles = from item in di.GetFiles()
                                where item.Extension == saveFileNameExtension
                                select item;

            return selectedFiles.ToArray();
        }
        else
        {
            return null;
        }
    }

}
    