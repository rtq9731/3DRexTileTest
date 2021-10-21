using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class TileScript : MonoBehaviour, ITurnFinishObj
{
    [SerializeField] TileData data;

    public TileData Data
    {
        get { return data; }
    }

    PlayerScript owner = null;

    public PlayerScript Owner
    {
        get { return owner; }
    }


    public void Damage(MissileTypes.MissileWarheadType warhead)
    {
        DoMissileHitAct(warhead, this);
        data.Shield -= MainSceneManager.Instance.GetWarheadData(warhead).Atk;
        
        if(data.Shield <= 0)
        {
            ChangeOwner(null);
            data.Shield = data.MaxShield;
        }
    }
    public void Damage(int damage)
    {
        data.Shield -= damage;

        if (data.Shield <= 0)
        {
            ChangeOwner(null);
            data.Shield = data.MaxShield;
        }
    }

    private void DoMissileHitAct(MissileTypes.MissileWarheadType warhead, TileScript hitTile)
    {
        List<TileScript> tilesInMoreHitRange = MainSceneManager.Instance.tileChecker.FindTilesInRange(hitTile, 1);
        switch (warhead)
        {
            case MissileTypes.MissileWarheadType.WideDamageTypeWarhead1:
                for (int i = 0; i < 2; i++)
                {
                    if(tilesInMoreHitRange.Count >= 1)
                    {
                        TileScript randTile = tilesInMoreHitRange[UnityEngine.Random.Range(0, tilesInMoreHitRange.Count)];
                        tilesInMoreHitRange.Remove(randTile);
                        randTile.Damage(MainSceneManager.Instance.GetWarheadData(warhead).Atk - 20);
                    }
                }
                break;
            case MissileTypes.MissileWarheadType.WideDamageTypeWarhead2:
                for (int i = 0; i < 4; i++)
                {
                    if (tilesInMoreHitRange.Count >= 1)
                    {
                        TileScript randTile = tilesInMoreHitRange[UnityEngine.Random.Range(0, tilesInMoreHitRange.Count)];
                        tilesInMoreHitRange.Remove(randTile);
                        randTile.Damage(MainSceneManager.Instance.GetWarheadData(warhead).Atk - 10);
                    }
                }
                break;
            case MissileTypes.MissileWarheadType.ContinuousTypeWarhead1:
                break;
            case MissileTypes.MissileWarheadType.ContinuousTypeWarhead2:
                break;
            case MissileTypes.MissileWarheadType.MoreWideTypeWarhead:
                break;
            case MissileTypes.MissileWarheadType.WideContinuousTypeWarhead:
                break;
            case MissileTypes.MissileWarheadType.DamageContinousTypeWarhead:
                break;
            case MissileTypes.MissileWarheadType.HellFireTypeWarhead:
                break;
            default:
                break;
        }
    }

    public void SetPosition(Vector3 pos)
    {
        data.Position = pos;
        gameObject.transform.position = pos;
    }

    public void ChangeOwner(PlayerScript newOwner)
    {
        if(newOwner != null && newOwner != owner)
        {
            GetComponent<MeshRenderer>().material.color = newOwner.playerColor;
            foreach (var item in transform.GetComponentsInChildren<MeshRenderer>())
            {
                if(item.transform.parent.GetComponent<CloudObject>() == null)
                {
                    item.material.color = newOwner.playerColor;
                }
            }
            owner = newOwner;
            newOwner.AddTile(this);
        }
        else if(newOwner == null)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            foreach (var item in transform.GetComponentsInChildren<MeshRenderer>())
            {
                if (item.transform.parent.GetComponent<CloudObject>() == null)
                {
                    item.material.color = Color.white;
                }
            }
            owner.OwningTiles.Remove(this);
        }

        owner = newOwner;
    }

    public void TurnFinish()
    {
        owner.AddResource(data.Resource);
    }

    public void BuyTile(PlayerScript owner)
    {
        if(owner.IsTurnFinish)
        {
#if UNITY_EDITOR
            Debug.Log("하 어딜 이미 턴 끝냈잖아 ㅋㅋ");
#endif
            return;
        }

        if (owner.ResourceTank >= data.Price)
        {

            if (this.owner == owner)
            {
#if UNITY_EDITOR
                Debug.Log("이미 주인인뎁쇼");
#endif
                return;
            }

#if UNITY_EDITOR
            Debug.Log($"{owner.name} 이 타일번호 {this.data.tileNum} 를! 구매합니다.");
#endif

            ChangeOwner(owner);
            owner.AddResource(-data.Price);
            MainSceneManager.Instance.InfoPanel.RefreshTexts(this);
        }
    }

    //public void FireMissile(TileScript attackTile)
    //{
    //    attackTile.Damage(data.attackPower);
    //}

    public void SelectTile(GameObject tileVcam)
    {
        if (data.Position != transform.position)
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
        transform.DOMove(data.Position, 0.3f);
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
