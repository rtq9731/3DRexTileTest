using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TechTreeDatas : MonoBehaviour
{
    [SerializeField]
    private SkillTreeNode[] skillTreeNodes;

    private string filePath = "";
    private string fileName = "TestData.txt";

    public void LoadData()
    {
        filePath = Application.dataPath + "/" + fileName;
        using (StreamReader sr = new StreamReader(filePath))
        {
            string dataString = sr.ReadToEnd();
            Debug.Log(dataString);

            JsonUtility.FromJsonOverwrite(dataString, this);
            Debug.Log(skillTreeNodes.Length);
        }
    }

    public void SaveData()
    {
        filePath = Application.dataPath + "/" + fileName;
        skillTreeNodes = new SkillTreeNode[] { new SkillTreeNode(), new SkillTreeNode() };
        string dataString = JsonUtility.ToJson(this);

        Debug.Log(dataString);

        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(dataString);
        sw.Close();
    }

    private void Awake()
    {
        LoadData();
    }
}
