using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTypes : MonoBehaviour
{
    public enum MissileWarheadType
    {
        CommonTypeWarhead,
        WideDamageTypeWarhead1,
        WideDamageTypeWarhead2,
        ContinuousTypeWarhead1,
        ContinuousTypeWarhead2,
        DamageTypeWarhead1,
        DamageTypeWarhead2,
        MoreWideTypeWarhead,
        WideContinuousTypeWarhead,
        DamageContinousTypeWarhead,
        HellFireTypeWarhead
    }

    public enum MissileEngineType
    {
        commonEngine,
        Tier1Engine,
        Tier2Engine,
        Tier3Engine,
        Tier4Engine
    }
}
