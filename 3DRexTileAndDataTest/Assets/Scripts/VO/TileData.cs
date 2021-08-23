using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileType type = TileType.None;

    public Vector3 Position = Vector3.zero;

    public int price = 3;
    public int shield = 10;
    public int resource = 1;
    public int attackPower = 0;
}
