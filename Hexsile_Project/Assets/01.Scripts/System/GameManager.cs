using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    string filePath = "";
    string saveFileNameExtension = ".sav";

    public bool isLoadData = false;

    private void Awake()
    {
        filePath = Application.dataPath + "/Saves";
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
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

        string saveFileName = MainSceneManager.Instance.PlayerName + "_" + System.DateTime.Now.ToString("yyyy_mmm_dd") + saveFileNameExtension;
        string saveFilePath = filePath + "/" ;

        if (!Directory.Exists(saveFilePath))
        {
            Directory.CreateDirectory(saveFilePath);
        }

        using (StreamWriter sw = new StreamWriter(new FileStream(saveFilePath + saveFileName, FileMode.OpenOrCreate)))
        {
            sw.Write(dataString);
        }
    }

    public void LoadData()
    {

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
    