using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeMissileInfo : MonoBehaviour
{
    [SerializeField] Text textMissileATK;
    [SerializeField] Text textMissileRange;
    [SerializeField] Text textMissileTurn;

    public void InitPanelMissileInfo(MissileData data)
    {
        textMissileATK.text = $"{data.WarHeadDamage}";
        textMissileRange.text = $"{data.MissileRange}";
        textMissileTurn.text = $"{data.TurnForMissileReady}";
    }
}
