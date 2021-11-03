using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileType type = TileType.None;
    public int tileNum = -1;

    Vector3 position = Vector3.zero;

    int maxShield = 100;
    int price = 20;
    int shield = 100;
    int maxResource = 3;
    int resource = 3;

    public int MaxShield { get { return maxShield; } set { maxShield = value; } }
    public int Price { get { return price; } set { price = value; } }
    public int Shield { get { return shield; } set { shield = value; } }
    public int MaxResource { get { return maxResource; } set { maxResource = value; } }
    public int Resource { get { return resource; } set { resource = value; } }

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

    private void SetDataToPlain() // 지형 초기화용 함수
    {
        resource = 3;
        shield = 100;
    }
}
