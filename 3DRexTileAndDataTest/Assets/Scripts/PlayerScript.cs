using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    List<TileDataScript> owningTiles = new List<TileDataScript>();

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
