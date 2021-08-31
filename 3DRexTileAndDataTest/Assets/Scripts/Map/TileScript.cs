using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileScript : MonoBehaviour, ITurnFinishObj
{

    [SerializeField] TileData data;

    public TileData Data
    {
        get { return data; }
        private set { }
    }

    PlayerScript owner = null;

    public PlayerScript Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    private event Action TrunOverEvent;


    public void Damage(int damage)
    {
        data.shield -= damage;
        if(data.shield < 0)
        {
            owner = null;
        }
    }

    public void SetPosition(Vector3 pos)
    {
        data.position = pos;
        gameObject.transform.position = pos;
    }

    public void ChangeOwner(PlayerScript newOwner)
    {
        owner = newOwner;
    }

    public void TurnFinish()
    {
        if(TrunOverEvent != null)
        {
            TrunOverEvent();

            TrunOverEvent = null;
        }

        Owner.AddResource(data.resource);
    }

    public void UpgradeTileAttackPower()
    {
        TrunOverEvent += () => data.attackPower++;
        Owner.AddResource(-1);
    }

    public void FireMissile(TileScript attackTile)
    {
        attackTile.Damage(data.attackPower);
    }

    public void SelectTile(GameObject tileVcam)
    {
        this.transform.DOMoveY(this.transform.position.y + 0.3f, 0.5f);
        Vector3 camPos = tileVcam.transform.position;
        tileVcam.transform.position = new Vector3(camPos.x, camPos.y, transform.position.z - 5);
        tileVcam.SetActive(true);
    }
    
    public void SetType(ObjType objType)
    {
        switch (objType)
        {
            case ObjType.Tree:
                break;
            case ObjType.Mountain:
                break;
            case ObjType.Rock:
                break;
            default:
                break;
        }
    }

}
