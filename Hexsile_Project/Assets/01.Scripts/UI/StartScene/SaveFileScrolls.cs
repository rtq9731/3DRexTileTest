using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileScrolls : MonoBehaviour
{
    [SerializeField] GameObject saveInfoPanel = null;
    [SerializeField] Transform panelParent = null;
        
    private void Start()
    {
        LoadAllSaves();
    }

    private void LoadAllSaves()
    {
        List<SaveData> datas = new List<SaveData>();

        foreach (var item in GameManager.Instance.GetAllSaveFiles())
        {
            datas.Add(GameManager.Instance.FileToData(item));
        }

        foreach (var item in datas)
        {
            GameObject temp = Instantiate(saveInfoPanel, panelParent);
            temp.GetComponent<SaveInfoPanel>().InitInfoPanel(item);
        }
    }
}
