using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissileData
{
    [SerializeField]
    int missilePrice = 0;
    public int MissilePrice
    {
        get { return missilePrice; }
        set { missilePrice = value; }
    }

    [SerializeField]
    int missileRange = 0;

    public int MissileRange
    {
        get
        {
            UpdateRange(MainSceneManager.Instance.GetEngineData(engineTier).Weight, MainSceneManager.Instance.GetWarheadData(warheadType).Weight);
            return missileRange; 
        }
        set { missileRange = value; }
    }

    [SerializeField]
    int warHeadDamage;

    public int WarHeadDamage
    {
        get { return warHeadDamage; }
        set { warHeadDamage = value; }
    }

    [SerializeField]
    int turnForMissileReady;
    
    public int TurnForMissileReady
    {
        get { return turnForMissileReady; }
        set { turnForMissileReady = value; }
    }

    [SerializeField]
    MissileTypes.MissileBody bodyType;

    public MissileTypes.MissileBody BodyType
    {
        get { return bodyType; }
        set
        {
            bodyType = value;
            UpdateRange(MainSceneManager.Instance.GetEngineData(engineTier).Weight, MainSceneManager.Instance.GetWarheadData(warheadType).Weight);
            UpdateTurnForFinish(MainSceneManager.Instance.GetMissileBodyData(bodyType).Makingtime);
        }
    }

    [SerializeField]
    MissileTypes.MissileEngineType engineTier;

    public MissileTypes.MissileEngineType EngineTier
    {
        get { return engineTier; }
        set
        {
            MissileTypes.MissileEngineType tmp = engineTier;

            engineTier = value;
            UpdateRange(MainSceneManager.Instance.GetEngineData(engineTier).Weight, MainSceneManager.Instance.GetWarheadData(warheadType).Weight);
            UpdateTurnForFinish(MainSceneManager.Instance.GetEngineData(engineTier).Makingtime);

            if (!CanMakeIt())
            {
                PanelException.CallExecptionPanel("�̻����� ���԰� �ʰ��Ͽ� ���ư��� �����ϴ�!\n���� �� �� ���� ���Դϴ�!\n����Ͻðڽ��ϱ�?",
                    () => { },
                    "���",
                    () =>
                    {
                        EngineTier = tmp;
                        MainSceneManager.Instance.missileMakerPanel.RefreshMissileInfoTexts();
                    },
                    "���");
            }
        }
    }

    [SerializeField]
    MissileTypes.MissileWarheadType warheadType;

    public MissileTypes.MissileWarheadType WarheadType
    {
        get { return warheadType; }
        set {
            MissileTypes.MissileWarheadType tmp = warheadType;

            warheadType = value;
            warHeadDamage = MainSceneManager.Instance.GetWarheadData(warheadType).Atk;
            UpdateRange(MainSceneManager.Instance.GetEngineData(engineTier).Weight, MainSceneManager.Instance.GetWarheadData(warheadType).Weight);
            UpdateTurnForFinish(MainSceneManager.Instance.GetWarheadData(warheadType).Makingtime);

            if(!CanMakeIt())
            {
                PanelException.CallExecptionPanel("�̻����� ���԰� �ʰ��Ͽ� ���ư��� �����ϴ�!\n���� �� �� ���� ���Դϴ�!\n����Ͻðڽ��ϱ�?", 
                    () => { }, 
                    "���", 
                    () =>
                    {
                        WarheadType = tmp;
                        MainSceneManager.Instance.missileMakerPanel.RefreshMissileInfoTexts();
                    }, 
                    "���");
            }
        }
    }

    public bool CanMakeIt()
    {
#if UNITY_EDITOR
        Debug.Log(MainSceneManager.Instance.GetEngineData(engineTier).Weight > MainSceneManager.Instance.GetWarheadData(warheadType).Weight);
#endif

        return MainSceneManager.Instance.GetEngineData(engineTier).Weight > MainSceneManager.Instance.GetWarheadData(warheadType).Weight;
    }

    private void UpdateRange(int engineWeight, int warheadWeight)
    {
        missileRange = engineWeight - warheadWeight;
        missileRange += MainSceneManager.Instance.GetMissileBodyData(bodyType).Morerange;
    }

    private void UpdateTurnForFinish(int makeTime)
    {
        if(TurnForMissileReady < makeTime)
        {
            TurnForMissileReady = makeTime;
        }
    }

    private void UpdatePrice()
    {
        int warheadPrice = MainSceneManager.Instance.GetWarheadData(warheadType).Price;
        int bodyPrice = MainSceneManager.Instance.GetMissileBodyData(bodyType).Price;
        int enginePrice = MainSceneManager.Instance.GetEngineData(engineTier).Price;


    }

    public MissileData(MissileTypes.MissileEngineType engine, MissileTypes.MissileWarheadType warheadType, MissileTypes.MissileBody body)
    {
        this.engineTier = engine;
        this.warheadType = warheadType;
        this.bodyType = body;
        EngineTier = engine;
        WarheadType = warheadType;
    }
}
