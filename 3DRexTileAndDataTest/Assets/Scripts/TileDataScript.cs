using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataScript : MonoBehaviour, ITurnFinishObj
{

    [SerializeField] TileData data = new TileData();
    PlayerScript Owner = null;

    private event Action g;

    public void Damage(int damage)
    {
        data.shield -= damage;
        if(data.shield < 0)
        {
            Owner = null;
        }
    }

    public void ChangeOwner(PlayerScript newOwner)
    {
        Owner = newOwner;
    }

    public void TurnFinish()
    {
        if(g != null)
        {
            g();

            g = null;
        }

        Owner.AddResource(data.resource);
    }

    public void UpgradeTileAttackPower()
    {
        g += () => data.attackPower++;
        Owner.AddResource(-1);
    }

    public void FireMissile(TileDataScript attackTile)
    {
        attackTile.Damage(data.attackPower);
    }

}
