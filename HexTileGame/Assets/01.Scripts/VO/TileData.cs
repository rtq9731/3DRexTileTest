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
    int range = 3;
    int price = 20;
    int shield = 100;
    int maxResource = 3;
    int resource = 3;
    int attackPower = 0;

    public int MaxShield { get { return maxShield; } set { maxShield = value; } }
    public int Range { get { return range; } set { range = value; } }
    public int Price { get { return price; } set { price = value; } }
    public int Shield { get { return shield; } set { shield = value; } }
    public int MaxResource { get { return maxResource; } set { maxResource = value; } }
    public int Resource { get { return resource; } set { resource = value; } }
    public int AttackPower { get { return attackPower; } set { attackPower = value; } }

    public Vector3 Position { get { return position; } set { position = value; } }

    public void SetDataToForest()
    {
        SetDataToPlain();
        type = TileType.Forest;
        maxShield += 10;
        shield = maxShield;
        range -= 1;
    }

    public void SetDataToMountain()
    {
        SetDataToPlain();
        type = TileType.Mountain;
        range += 1;
        maxResource -= 1;
        resource -= 1;
    }

    public void SetDataToDigSite()
    {
        SetDataToPlain();
        type = TileType.DigSite;
        maxResource += 1;
        resource += 1;
        range -= 1;
    }

    public void SetDataToRock()
    {
        maxShield += 30;
        shield = maxShield;
        attackPower -= 1;
    }

    private void SetDataToPlain() // 지형 초기화용 함수
    {
        range = 3;
        resource = 3;
        shield = 100;
    }
}
