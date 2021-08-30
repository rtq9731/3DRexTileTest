using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapData : MonoBehaviour
{
    public static TileMapData Instance;

    private List<TileScript> tileList = new List<TileScript>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public TileScript GetTile(int num)
    {
        return tileList[num];
    }

    public void SetTileData(TileScript item)
    {
        tileList.Add(item);
    }

    public TileScript GetRandTile(PlayerScript player)
    {
        TileScript result = tileList[Random.Range(0, tileList.Count)];

        if(result.Data.type == (TileType.Ocean | TileType.Lake | TileType.None)) // ������Ʈ ���°� �Ұ����� �������� üũ
        {
            if(result.Owner == null) // ���� ���°� �´��� üũ
            {
                result.Owner = player;
                return result;
            }
        }

        return GetRandTile(player);
    }
}
