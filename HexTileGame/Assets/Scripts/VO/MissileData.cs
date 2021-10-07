using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissileData
{
    public int warHeadDamage;

    public int turnForMissileReady;

    public MissileTypes.MissileEngineType engineTier;

    public MissileTypes.MissileWarheadType warheadType;

    public MissileData(int turn, MissileTypes.MissileEngineType engine, MissileTypes.MissileWarheadType warheadType)
    {
        this.turnForMissileReady = turn;
        this.engineTier = engine;
        this.warheadType = warheadType;
    }
}
