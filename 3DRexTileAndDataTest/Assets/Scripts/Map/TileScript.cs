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

    public void BuyTile(PlayerScript owner)
    {
        if(this.owner == owner)
        {
#if UNITY_EDITOR
            Debug.Log("ÀÌ¹Ì ÁÖÀÎÀÎµª¼î");
#endif
            return;
        }
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
        if (data.position != transform.position)
        {
            return;
        }

        DOTween.Complete(this.transform);
        DOTween.Kill(tileVcam.transform);
        this.transform.DOMoveY(this.transform.position.y + 0.3f, 0.5f);
        transform.DOLocalRotate(new Vector3(0, 360, 0), 0.3f);
        tileVcam.transform.DOMove(new Vector3(transform.position.x + 1, 2.5f, transform.position.z - 1), 0.3f);
        tileVcam.SetActive(true);
    }

    public void RemoveSelect(GameObject tileVcam)
    {
        tileVcam.SetActive(false);
        transform.DOMove(data.position, 0.3f);
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
