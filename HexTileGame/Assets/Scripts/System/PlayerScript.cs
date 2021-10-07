using System;
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

    public List<TileScript> owningTiles = new List<TileScript>();

    public List<MissileScript> missileInMaking = new List<MissileScript>();
    public List<MissileScript> missileReadyToShoot = new List<MissileScript>();

    bool isTurnFinish = false;
    public bool IsTurnFinish { get { return isTurnFinish; } }

    int resouceTank = 0;
    public int ResourceTank { get { return resouceTank; } }

    private void Awake()
    {
        TurnFinishAction = null;
    }

    protected void Start()
    {
        MainSceneManager.Instance.Players.Add(this);
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    public void StartNewTurn()
    {
        isTurnFinish = false;
    }

    public void AddResource(int resource)
    {
        resouceTank += resource;
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    public void TurnFinish()
    {
        if (isTurnFinish)
        {
            return;
        }
        else
        {
            isTurnFinish = true;

            TurnFinishAction();
            owningTiles.ForEach(x => x.TurnFinish());
            MainSceneManager.Instance.CheckTurnFinish();
        }
    }

    public void AddTile(TileScript tile)
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
