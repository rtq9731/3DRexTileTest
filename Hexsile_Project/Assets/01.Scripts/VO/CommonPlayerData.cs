using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CommonPlayerData : ISerializationCallbackReceiver
{
    [SerializeField] protected List<TileScript> owningTiles = new List<TileScript>();

    [SerializeField] protected int[] tileNums = null;
    [SerializeField] protected bool isGameOver = false;
    [SerializeField] protected string playerName = "None";
    [SerializeField] protected Color color = Color.white;

    public int[] TileNums
    {
        get { return tileNums; }
    }
    public List<TileScript> OwningTiles
    {
        get { return owningTiles; } set { owningTiles = value; }
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
        get { return playerName; } set { playerName = value; }
    }
    public Color PlayerColor
    {
        get { return color; } set { color = value;}
    }

    public void OnBeforeSerialize()
    {
        if(owningTiles.Count < 1)
        {
            tileNums = new int[] { };
        }

        List<int> list = new List<int>();
        owningTiles.ForEach(x => list.Add(x.Data.tileNum));
        tileNums = list.ToArray();
    }

    public void OnAfterDeserialize()
    {

    }
}
