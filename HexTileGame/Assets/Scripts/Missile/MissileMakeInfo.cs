using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileMakeInfo : MonoBehaviour
{
    [SerializeField] Text textMissileWarhead;
    [SerializeField] Text textMissileMakeTime;

    public void SetData(MissileData data)
    {
        textMissileWarhead.text = $"{MainSceneManager.Instance.GetWarheadData(data.WarheadType)} 생산 중";
        textMissileMakeTime.text = $"{data.TurnForMissileReady} 턴 뒤에 생산";
    }
}
