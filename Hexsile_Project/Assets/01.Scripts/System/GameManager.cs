using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    string filePath = "";
    
    public bool isLoadData = false;

    private void Awake()
    {
        filePath = Application.dataPath + "Saves";
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SaveData()
    {
        JsonUtility.ToJson(new SaveData(TileMapData.Instance.GetAllTilesData(), MainSceneManager.Instance.GetPlayer().PlayerData, AIManager.Instance.GetAIDatas(), MainSceneManager.Instance.turnCnt, MainSceneManager.Instance.stageCount, MainSceneManager.Instance.mapSize, MainSceneManager.Instance.isRerolled));
        
        using (StreamWriter sw = new StreamWriter(filePath))
        {

        }
    }

    public void LoadData()
    {

    }

    public FileInfo[] GetAllSaveFiles()
    {
        if (Directory.Exists(filePath))
        {
            DirectoryInfo di = new DirectoryInfo(filePath);

            var selectedFiles = from item in di.GetFiles()
                                where item.Name.Contains(".sav")
                                select item;

            return selectedFiles.ToArray();
        }
        else
        {
            return null;
        }
    }

}
    