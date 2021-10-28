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

    public void ResetTileList()
    {
        tileList = new List<TileScript>();
    }

    public TileScript GetTile(int num)
    {
        return tileList[num];
    }

    public void SetTileData(TileScript item)
    {
        tileList.Add(item);
    }

    public List<TileScript> GetAllTiles()
    {
        return tileList;
    }

    public void ResetColorAllTile()
    {
        foreach (var item in tileList)
        {
            item.GetComponent<MeshRenderer>().material.color = item.Owner != null ? item.Owner.playerColor : Color.white;
        }
    }

    public TileScript GetRandTile()
    {
        TileScript result = tileList[Random.Range(0, tileList.Count)];

        if(result.Data.type != (TileType.Ocean | TileType.Lake | TileType.None)) // ������Ʈ ���°� �Ұ����� �������� üũ
        {
            if(result.Owner == null) // ���� ���°� �´��� üũ
            {
                return result;
            }
        }

        return GetRandTile();
    }
}
