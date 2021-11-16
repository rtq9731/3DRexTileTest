using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SaveTester : Editor 
{
    [MenuItem("Test/SaveData")]
    static void SaveData()
    {
        GameManager.Instance.SaveData();
    }
    [MenuItem("Test/KillAllAI")]
    static void KillAI()
    {
        TileMapData.Instance.GetAllTiles().FindAll(x => x.Owner != MainSceneManager.Instance.GetPlayer()).ForEach(x => x.Damage(200));
    }
}
