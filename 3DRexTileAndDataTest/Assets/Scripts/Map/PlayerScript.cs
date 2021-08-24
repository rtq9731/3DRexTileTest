using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    private string ownerName = "None";

    public string OwnerName
    {
        get { return ownerName; }
    }

    List<TileScript> owningTiles = new List<TileScript>();

    int resouceTank = 0;

    public void AddResource(int resource)
    {
        resouceTank += resource;
    }

    public void TurnFinish()
    {
        owningTiles.ForEach(x => x.TurnFinish());
    }

}
