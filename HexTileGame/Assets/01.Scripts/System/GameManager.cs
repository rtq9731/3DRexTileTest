using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool isLoadData = false;

    public void SaveData()
    {
        TileMapData.Instance.GetAllTiles();
    }

    public void LoadData()
    {
        
    }

}
    