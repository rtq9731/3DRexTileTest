using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    public abstract string MyName
    {
        get;
    }
    public abstract List<TileScript> OwningTiles
    {
        get;
    }

    public Action TurnFinishAction;

    public abstract void ResetPlayer();
    public abstract void AddTile(TileScript tile);
    public abstract void RemoveTile(TileScript tile);


    protected void Awake()
    {
        TurnFinishAction = () => { }; // 액션 초기화
    }

    public virtual void TurnFinish()
    {
        TurnFinishAction();
    }

}
