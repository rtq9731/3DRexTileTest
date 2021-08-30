using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    private string myName = "None";

    public string MyName
    {
        get { return myName; }
    }

    List<TileScript> owningTiles = new List<TileScript>();

    int resouceTank = 0;

    private void Awake()
    {
        GameManager.Instance.Players.Add(this);
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
        owningTiles.Add(tile);
    }
}
