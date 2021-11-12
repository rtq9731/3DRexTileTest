using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileScrolls : MonoBehaviour
{
    [SerializeField] GameObject saveInfoPanel = null;
        
    private void Start()
    {

    }

    private void LoadAllSaves()
    {
        foreach (var item in GameManager.Instance.GetAllSaveFiles())
        {
            using (StreamReader sr = item.OpenText())
            {
                SaveData data = JsonUtility.FromJson<SaveData>(sr.ReadToEnd());
            }
        }
    }
}
