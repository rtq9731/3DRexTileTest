using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : ISerializationCallbackReceiver
{
    public DateTime saveTime = DateTime.Now;
    public string saveTimeString = "";

    public MainPlayerData playerData = null;
    public TileData[] tiles = null;
    public AIData[] aiPlayers = null;

    public uint turnCnt = 0;
    public uint stageCount = 1;
    public int mapSize = 3;

    public bool isRerolled = false;

    public SaveData(TileData[] tiles, MainPlayerData playerData, AIData[] aiPlayers, uint turnCnt, uint stageCount, int mapSize, bool isRerolled)
    {
        this.tiles = tiles;
        this.playerData = playerData;
        this.aiPlayers = aiPlayers;
        this.turnCnt = turnCnt;
        this.stageCount = stageCount;
        this.mapSize = mapSize;
        this.isRerolled = isRerolled;
        saveTime = System.DateTime.Now;
    }

    public void OnBeforeSerialize()
    {
        saveTimeString = System.DateTime.Now.ToString();
    }

    public void OnAfterDeserialize()
    {
        saveTime = DateTime.Parse(saveTimeString);
    }
}
