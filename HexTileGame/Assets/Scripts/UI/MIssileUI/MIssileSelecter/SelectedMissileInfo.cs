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
        MissileWarheadData warheadData = MissileWarhead.GetWarheadData(data.WarheadType);
        textMissileWarheadType.text = $"������ �̻��� ź�� : {warheadData.Name}";
        textMissileRange.text = $"�̻����� ��Ÿ� : { data.MissileRange }";
        textInfoWarheadName.text = $"{warheadData.Name}�� Ư¡ : ";
        textMissileInfo.text = warheadData.Info;
    }
}
