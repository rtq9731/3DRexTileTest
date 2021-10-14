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
        textMissileWarheadType.text = $"������ �̻��� ź�� : {data.WarheadType}";
        textMissileRange.text = $"�̻����� ��Ÿ� : { data.EngineTier }";
        textInfoWarheadName.text = $"{data.WarheadType}�� Ư¡ : ";
        textMissileInfo.text = data.MissileInfo;
    }
}
