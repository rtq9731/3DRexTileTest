using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissileData
{
    [SerializeField]
    int missileRange = 0;
    
    public int MissileRange
    {
        get { return missileRange; }
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
    MissileTypes.MissileEngineType engineTier;

    public MissileTypes.MissileEngineType EngineTier
    {
        get { return engineTier; }
        set { engineTier = value; }
    }

    [SerializeField]
    MissileTypes.MissileWarheadType warheadType;
    public MissileTypes.MissileWarheadType WarheadType
    {
        get { return warheadType; }
        set { warheadType = value; }
    }

    [SerializeField]
    string missileInfo;
    public string MissileInfo
    {
        get { return missileInfo; }
        set { missileInfo = value; }
    }



    public MissileData(int turn, MissileTypes.MissileEngineType engine, MissileTypes.MissileWarheadType warheadType)
    {
        this.turnForMissileReady = turn;
        this.engineTier = engine;
        this.warheadType = warheadType;
        missileRange = MissileEngine.GetEngineData(engineTier).Weight - MissileWarhead.GetWarheadData(warheadType).Weight;
        warHeadDamage = MissileWarhead.GetWarheadData(warheadType).Atk;
    }
}
