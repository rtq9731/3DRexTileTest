using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommonPlayerData
{
    [SerializeField] protected List<TileScript> owningTiles = new List<TileScript>();
    [SerializeField] protected bool isGameOver = false;
    [SerializeField] string playerName = "None";
    [SerializeField] Color color = Color.white;

    public List<TileScript> OwningTiles
    {
        get { return owningTiles; }
    }
    public CommonPlayerData()
    {
        owningTiles = new List<TileScript>();
    }
    public string PlayerName
    {
        get { return playerName; }
    }
    public Color playerColor
    {
        get { return playerColor; }
    }
}
