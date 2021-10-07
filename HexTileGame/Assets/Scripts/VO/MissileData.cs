using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissileData
{
    public int WarHeadDamage
    {
        get { return WarHeadDamage; } set { WarHeadDamage = value; }
    }

    public int TurnForMissileReady
    {
        get { return TurnForMissileReady; } set { TurnForMissileReady = value; }
    }

    public MissileTypes.MissileEngineType EngineTier
    {
        get { return EngineTier; } set { EngineTier = value; }
    }

    public MissileTypes.MissileWarheadType WarheadType
    {
        get { return WarheadType; } set { WarheadType = value; }
    }

    public MissileData(int turn, MissileTypes.MissileEngineType engine, MissileTypes.MissileWarheadType warheadType)
    {
        TurnForMissileReady = turn;
        EngineTier = engine;
        WarheadType = warheadType;
    }
}
