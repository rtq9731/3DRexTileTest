using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMissileQueueElement : MonoBehaviour
{
    [SerializeField] Text textMissileWarhead;
    [SerializeField] Text textMissileMakeTime;
    
    public void SetData(MissileData data)
    {
        Debug.Log("���� " + gameObject);
        textMissileWarhead.text = $"{data.warheadType} ���� ��";
        textMissileMakeTime.text = $"{data.turnForMissileReady} �� �ڿ� ����";
    }
}
