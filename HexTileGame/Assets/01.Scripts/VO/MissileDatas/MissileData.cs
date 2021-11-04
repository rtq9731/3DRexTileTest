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
            UpdatePrice();
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
            UpdatePrice();

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
            UpdateRange(MainSceneManager.Instance.GetEngineData(engineTier).Weight, MainSceneManager.Instance.GetWarheadData(warheadType).Weight);
            UpdateTurnForFinish(MainSceneManager.Instance.GetWarheadData(warheadType).Makingtime);
            UpdatePrice();

            if (!CanMakeIt())
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
        return MainSceneManager.Instance.GetEngineData(engineTier).Weight >= MainSceneManager.Instance.GetWarheadData(warheadType).Weight;
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

        missilePrice = warheadPrice + bodyPrice + enginePrice;
    }

    public MissileData(MissileTypes.MissileEngineType engine, MissileTypes.MissileWarheadType warheadType, MissileTypes.MissileBody body)
    {
        this.engineTier = engine;
        this.warheadType = warheadType;
        this.bodyType = body;
        EngineTier = engine;
        WarheadType = warheadType;
        BodyType = bodyType;
    }
}
