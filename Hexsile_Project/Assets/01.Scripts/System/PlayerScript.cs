using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    protected string myName = "NULL";

    public string MyName
    {
        get { return myName; }
    }

    public Action TurnFinishAction;
    public abstract void ResetPlayer();
    public abstract void AddTile(TileScript tile);
    public abstract void RemoveTile(TileScript tile);

    public virtual List<TileScript> OwningTiles
    {
        get 
        { 
            return null; 
        }
    }

    protected void Awake()
    {
        TurnFinishAction = () => { }; // 액션 초기화
    }


    public virtual void TurnFinish()
    {
        TurnFinishAction();
    }

}
