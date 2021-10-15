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

    private void OnEnable()
    {
        RefreshTextsToNull();
    }

    private void RefreshTextsToNull()
    {
        textMissileWarheadType.text = "선택된 미사일 없음";
        textMissileRange.text = "";
        textInfoWarheadName.text = "";
        textMissileInfo.text = "";
    }

    public void RefreshTexts(MissileData data)
    {
        MissileWarheadData warheadData = MainSceneManager.Instance.GetWarheadData(data.WarheadType);
        textMissileWarheadType.text = $"장착된 미사일 탄두 : {warheadData.Name}";
        textMissileRange.text = $"미사일의 사거리 : { data.MissileRange }";
        textInfoWarheadName.text = $"{warheadData.Name}의 특징 : ";
        textMissileInfo.text = warheadData.Info;
    }
}
