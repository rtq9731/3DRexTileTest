using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    protected string myName = "NULL";
    public Color playerColor;

    [SerializeField] protected CommonPlayerData data = new CommonPlayerData();

    public string MyName
    {
        get { return myName; }
    }

    public Action TurnFinishAction;

    public virtual List<TileScript> OwningTiles
    {
        get 
        { 
            return data.OwningTiles; 
        }
    }

    protected void Awake()
    {
        TurnFinishAction = () => { }; // �׼� �ʱ�ȭ
    }

    public virtual void ResetPlayer()
    {
        data.OwningTiles.Clear();
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

        // �ߺ� �ȵǰ� �ϱ� ����.
        data.OwningTiles.Remove(tile);
        data.OwningTiles.Add(tile);
    }

    public virtual void RemoveTile(TileScript tile)
    {
        data.OwningTiles.Remove(tile);
    }
}
