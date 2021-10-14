using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

///
/// !!! Machine generated code !!!
///
/// A class which deriveds ScritableObject class so all its data 
/// can be serialized onto an asset data file.
/// 
[System.Serializable]
public class MissileWarhead : ScriptableObject 
{
    static MissileWarhead instance = null;

    [HideInInspector] [SerializeField] 
    public string SheetName = "";
    
    [HideInInspector] [SerializeField] 
    public string WorksheetName = "";
    
    // Note: initialize in OnEnable() not here.
    public MissileWarheadData[] dataArray;
    private List<MissileWarheadData> dataList = new List<MissileWarheadData>();

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    void OnEnable()
    {		
//#if UNITY_EDITOR
        //hideFlags = HideFlags.DontSave;
//#endif
        // Important:
        //    It should be checked an initialization of any collection data before it is initialized.
        //    Without this check, the array collection which already has its data get to be null 
        //    because OnEnable is called whenever Unity builds.
        // 		
        if (dataArray == null)
            dataArray = new MissileWarheadData[0];

        for (int i = 0; i < dataArray.Length; i++)
        {
            dataList.Add(dataArray[i]);
        }

    }
    
    //
    // Highly recommand to use LINQ to query the data sources.
    //

    public static MissileWarheadData GetWarheadData(MissileTypes.MissileWarheadType type)
    {
        return instance.dataList.Find(x => x.TYPE == type);
    }

    public static MissileWarheadData GetWarheadByIdx(int idx)
    {
        return instance.dataArray[idx];
    }

}
