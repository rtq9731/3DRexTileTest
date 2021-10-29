using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    [SerializeField] protected string myName = "NULL";
    public Color playerColor;

    public string MyName
    {
        get { return myName; }
    }

    public Action TurnFinishAction;

    [SerializeField]
    protected List<TileScript> owningTiles = new List<TileScript>();
    public List<TileScript> OwningTiles
    {
        get { return owningTiles; }
    }

    protected List<MissileData> missileInMaking = new List<MissileData>();
    public  List<MissileData> MissileInMaking
    {
        get { return missileInMaking; }
    }

    protected List<MissileData> missileReadyToShoot = new List<MissileData>();
    public List<MissileData> MissileReadyToShoot
    {
        get { return missileReadyToShoot; }
    }

    protected int researchFinishTurn = 0;
    public int ResearchFinishTurn
    {
        get { return researchFinishTurn; }
    }

    protected int resouceTank = 0;
    public int ResourceTank { get { return resouceTank; } }

    protected void Awake()
    {
        TurnFinishAction = () => { }; // 액션 초기화

        TurnFinishAction += () => { MainSceneManager.Instance.turnCnt++; };

        TurnFinishAction += () => {
            missileInMaking.ForEach(x => x.TurnForMissileReady--);
            missileInMaking.FindAll(x => x.TurnForMissileReady <= 0).ForEach(x => {
                missileInMaking.Remove(x);
                missileReadyToShoot.Add(x);
                });
            };
    }

    public virtual void ResetPlayer()
    {
        owningTiles.Clear();
    }

    public void AddResource(int resource)
    {
        resouceTank += resource;
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    public virtual void TurnFinish()
    {
        TurnFinishAction();
    }

    public virtual void AddTile(TileScript tile)
    {
        if(tile.Owner != this)
        {
            tile.ChangeOwner(this);
        }

        // 중복 안되게 하기 위함.
        owningTiles.Remove(tile);
        owningTiles.Add(tile);
    }
}
