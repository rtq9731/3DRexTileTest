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
                PanelException.CallExecptionPanel("미사일의 무게가 초과하여 날아갈수 없습니다!\n제작 할 수 없을 것입니다!\n계속하시겠습니까?",
                    () => { },
                    "계속",
                    () =>
                    {
                        EngineTier = tmp;
                        MainSceneManager.Instance.missileMakerPanel.RefreshMissileInfoTexts();
                    },
                    "취소");
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
                PanelException.CallExecptionPanel("미사일의 무게가 초과하여 날아갈수 없습니다!\n제작 할 수 없을 것입니다!\n계속하시겠습니까?", 
                    () => { }, 
                    "계속", 
                    () =>
                    {
                        WarheadType = tmp;
                        MainSceneManager.Instance.missileMakerPanel.RefreshMissileInfoTexts();
                    }, 
                    "취소");
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
