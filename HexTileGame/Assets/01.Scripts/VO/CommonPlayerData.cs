using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommonPlayerData
{
    [SerializeField] protected List<TileScript> owningTiles = new List<TileScript>();

    public List<TileScript> OwningTiles
    {
        get { return owningTiles; }
    }

    public CommonPlayerData()
    {
        owningTiles = new List<TileScript>();
    }
}
