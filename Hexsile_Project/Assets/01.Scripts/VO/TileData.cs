using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileType type = TileType.None;
    public int tileNum = -1;

    [SerializeField] Vector3 position = Vector3.zero;
    [SerializeField] int maxShield = 100;
    [SerializeField] int price = 20;
    [SerializeField] int shield = 100;
    [SerializeField] int maxResource = 3;
    [SerializeField] int resource = 3;
    [SerializeField] int resourceLossTurn = 0;
    [SerializeField] int resourceLoss = 0;
    [SerializeField] bool isInEffect = false;

    public int MaxShield { get { return maxShield; } set { maxShield = value; } }
    public int Price { get { return price; } set { price = value; } }
    public int Shield { get { return shield; } set { shield = value; } }
    public int MaxResource { get { return maxResource; } set { maxResource = value; } }
    public int Resource { get { return resource; } set { resource = value; } }
    public int ResourceLossTurn { get { return resourceLossTurn; } set { resourceLossTurn = value; } }
    public int ResourceLoss { get { return resourceLoss; } set { resourceLoss = value; } }
    public bool IsInEffect { get { return isInEffect; } set { isInEffect = value; } }

    public Vector3 Position { get { return position; } set { position = value; } }

    public void SetDataToForest()
    {
        SetDataToPlain();
        type = TileType.Forest;
        maxShield += 10;
        shield = maxShield;
    }

    public void SetDataToMountain()
    {
        SetDataToPlain();
        type = TileType.Mountain;
        maxResource -= 1;
        resource -= 1;
    }

    public void SetDataToDigSite()
    {
        SetDataToPlain();
        type = TileType.DigSite;
        maxResource += 1;
        resource += 1;
    }

    public void SetDataToRock()
    {
        maxShield += 30;
        shield = maxShield;
    }

    public void SetDataToOcean()
    {
        SetDataToPlain();
        type = TileType.Ocean;
        price = 0;
        resource = 0;
        maxShield = 0;
        shield = 0;
    }
    public void SetDataToLake()
    {
        SetDataToPlain();
        type = TileType.Lake;
        price = 0;
        resource = 0;
        maxShield = 0;
        shield = 0;
    }

    private void SetDataToPlain() // 지형 초기화용 함수
    {
        type = TileType.Plain;
        price = 20;
        resource = 3;
        maxShield = 100;
        shield = 100;
    }

    public TileData()
    {
        SetDataToPlain();
    }
}
