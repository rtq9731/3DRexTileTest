using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileData
{
    public int TurnForMissileReady
    {
        get { return TurnForMissileReady; } set { TurnForMissileReady = value; }
    }

    public int EngineTier
    {
        get { return EngineTier; } set { EngineTier = value; }
    }

    public MissileTypes.MissileWarheadType missileWarheadType
    {
        get { return missileWarheadType; } set { missileWarheadType = value; }
    }
}
