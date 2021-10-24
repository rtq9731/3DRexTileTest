using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TechTreeDataManager : MonoBehaviour
{
    private string filePath = "";
    private string fileName = "TestData.txt";

    private void Awake()
    {
        Debug.Log(Application.dataPath);
        filePath = Application.dataPath + "/" + fileName;

        SkillTreeNode skillTreeNode = new SkillTreeNode();
        string dataString = JsonUtility.ToJson(skillTreeNode);

        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(dataString);
        sw.Close();
    }
}
