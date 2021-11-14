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
}
