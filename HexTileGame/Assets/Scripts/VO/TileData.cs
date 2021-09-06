using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileType type = TileType.None;

    public Vector3 position = Vector3.zero;

    public int range = 3;
    public int price = 5;
    public int shield = 1;
    public int resource = 3;
    public int attackPower = 0;

    public void SetDataToForest()
    {
        SetDataToPlain();
        type = TileType.Forest;
        shield += 1;
        range -= 1;
    }

    public void SetDataToMountain()
    {
        SetDataToPlain();
        type = TileType.Mountain;
        range += 1;
        resource -= 1;
    }

    public void SetDataToDigSite()
    {
        SetDataToPlain();
        type = TileType.DigSite;
        resource += 1;
        range -= 1;
    }

    public void SetDataToRock()
    {
        shield += 3;
        attackPower -= 1;
    }

    private void SetDataToPlain() // 지형 초기화용 함수
    {
        range = 3;
        resource = 3;
        shield = 1;
    }
}
