using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : ISerializationCallbackReceiver
{
    DateTime saveTime = DateTime.Now;

    public MainPlayerData playerData = null;
    public TileData[] tiles = null;
    public CommonPlayerData[] aiPlayers = null;

    public uint turnCnt = 0;
    public uint stageCount = 1;
    public int mapSize = 3;

    public bool isRerolled = false;

    public SaveData(TileData[] tiles, MainPlayerData playerData, CommonPlayerData[] aiPlayers, uint turnCnt, uint stageCount, int mapSize, bool isRerolled)
    {
        this.tiles = tiles;
        this.playerData = playerData;
        this.aiPlayers = aiPlayers;
        this.turnCnt = turnCnt;
        this.stageCount = stageCount;
        this.mapSize = mapSize;
        this.isRerolled = isRerolled;
    }

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        saveTime = DateTime.Now;
    }
}
