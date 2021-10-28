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

    public void RemoveTileOnList(TileScript item)
    {
        tileList.Remove(item);
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

    public List<TileScript> GetEndTile(int size)
    {
        List<TileScript> result = new List<TileScript>();
        for (int i = 0; i < size; i++)
        {
            result.Add(tileList.Find(x => 
            x.Data.type != (TileType.Ocean | TileType.Lake | TileType.None)
            && !result.Contains(x)
            && MainSceneManager.Instance.tileChecker.FindTilesInRange(x, 1).Count == 3));
        }

        return result;
    }
}
