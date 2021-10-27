using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMissileInfo : MonoBehaviour
{
    [SerializeField] Text textWarhead;
    [SerializeField] Text textEngine;
    [SerializeField] Text textBody;
    [SerializeField] Text textMissileATK;
    [SerializeField] Text textMissileRange;
    [SerializeField] Text textMissileTurn;

    public void InitPanelMissileInfo(MissileData data)
    {
        textWarhead.text = $"������ ź�� : {MainSceneManager.Instance.GetWarheadData(data.WarheadType).Name}";
        textEngine.text = $"������ ���� : {MainSceneManager.Instance.GetEngineData(data.EngineTier).Name}";
        textBody.text = $"������ ��ü : {MainSceneManager.Instance.GetMissileBodyData(data.BodyType).Name}";
        textMissileATK.text = $"�̻��� ���ݷ� : {data.WarHeadDamage}";
        textMissileRange.text = $"�̻��� ��Ÿ� : {data.MissileRange}";
        textMissileTurn.text = $"�̻��� ���꿡 �ʿ��� ��  : {data.TurnForMissileReady}";
    }
}
