using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CommonPlayerData
{
    protected List<TileScript> owningTiles = new List<TileScript>();

    [SerializeField] protected bool isGameOver = false;
    [SerializeField] string playerName = "None";
    [SerializeField] Color color = Color.white;

    public List<TileScript> OwningTiles
    {
        get { return owningTiles; }
    }
    public bool IsGameOver
    {
        get { return isGameOver; } set { isGameOver = value; }
    }
    public CommonPlayerData()
    {
        owningTiles = new List<TileScript>();
    }
    public string PlayerName
    {
        get { return playerName; }
    }
    public Color PlayerColor
    {
        get { return color; }
    }
}
