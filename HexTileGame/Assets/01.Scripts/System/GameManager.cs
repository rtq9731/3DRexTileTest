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
        TileMapData.Instance.GetAllTiles();

    }

    public void LoadData()
    {
        if (Directory.Exists(filePath))
        {
            DirectoryInfo di = new DirectoryInfo(filePath);

        }
    }


}
    