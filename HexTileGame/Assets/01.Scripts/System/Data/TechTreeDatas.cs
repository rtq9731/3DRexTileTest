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

    private List<SkillTreeNode> dataList = new List<SkillTreeNode>();

    MainPlayerData data = new MainPlayerData();

    private string dataString = "";

    public void LoadData()
    {
        dataString = Resources.Load("Data/ResearchData").ToString();
        JsonUtility.FromJsonOverwrite(dataString, this);

#if UNITY_EDITOR
        Debug.Log("불러온 데이터 : \n" + dataString);   
#endif

        for (int i = 0; i < skillTreeNodes.Length; i++)
        {
            dataList.Add(skillTreeNodes[i]);
        }
    }

    public SkillTreeNode GetDataByIdx(int idx)
    {
        return dataList.Find(x => x.Idx == idx);
    }

    private void Awake()
    {
        LoadData();
    }
}
