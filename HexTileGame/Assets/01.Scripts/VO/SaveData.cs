using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<TileScript> tiles = new List<TileScript>();
    public MainPlayerData playerData = null;
    public CommonPlayerData[] aiPlayers = null;

    public uint turnCnt = 0;
    public uint stageCount = 1;
    public int mapSize = 3;

    public bool isRerolled = false;
}
