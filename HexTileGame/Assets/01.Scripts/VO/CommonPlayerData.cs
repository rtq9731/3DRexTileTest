using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommonPlayerData
{
    [SerializeField] protected List<TileScript> owningTiles = new List<TileScript>();
    [SerializeField] protected bool isGameOver = false;

    public List<TileScript> OwningTiles
    {
        get { return owningTiles; }
    }

    public CommonPlayerData()
    {
        owningTiles = new List<TileScript>();
    }
}
