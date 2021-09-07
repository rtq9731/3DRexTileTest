using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    [SerializeField] protected string myName = "NULL";

    public string MyName
    {
        get { return myName; }
    }

    public List<TileScript> owningTiles = new List<TileScript>();

    int resouceTank = 0;
    public int ResourceTank { get { return resouceTank; } }

    protected void Start()
    {
        MainSceneManager.Instance.Players.Add(this);
    }

    public void AddResource(int resource)
    {
        resouceTank += resource;
    }

    public void TurnFinish()
    {
        owningTiles.ForEach(x => x.TurnFinish());
    }

    public void AddTile(TileScript tile)
    {
        tile.Owner = this;
        owningTiles.Add(tile);
    }
}
