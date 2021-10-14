using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMissileInfo : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text textMissileWarheadType;
    [SerializeField] Text textMissileRange;
    [SerializeField] Text textInfoWarheadName;
    [SerializeField] Text textMissileInfo;

    public void RefreshTexts(MissileData data)
    {
        textMissileWarheadType.text = $"장착된 미사일 탄두 : {data.WarheadType}";
        textMissileRange.text = $"미사일의 사거리 : { data.EngineTier }";
        textInfoWarheadName.text = $"{data.WarheadType}의 특징 : ";
        textMissileInfo.text = data.MissileInfo;
    }
}
