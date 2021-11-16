using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapData : MonoBehaviour
{
    private static TileMapData instance;

    [SerializeField] private List<TileScript> tileList = new List<TileScript>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static TileMapData Instance
    {
        get { return instance; }
    }

    public void SetTileList(List<TileScript> list)
    {
        tileList = list;
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
    public TileData[] GetAllTilesData()
    {
        List<TileData> tileDatas = new List<TileData>();
        tileList.ForEach(x => tileDatas.Add(x.Data));

        return tileDatas.ToArray();
    }

    public void ResetColorAllTile()
    {
        foreach (var item in tileList)
        {
            MeshRenderer borderMesh = item.GetComponent<TilePrefabScript>().meshBoder;

            if (borderMesh == null)
                continue;

            if (item.Owner != null)
            {
                if (item.Owner != MainSceneManager.Instance.GetPlayer())
                {
                    borderMesh.material.color = (item.Owner as AIPlayer).Data.PlayerColor;
                }
                else
                {
                    borderMesh.material.color = (item.Owner as PersonPlayer).PlayerData.PlayerColor;
                }
            }
            else
            {
                borderMesh.material.color = Color.white;
            }

        }
    }

    public List<TileScript> GetEndTile(int size)
    {
        List<TileScript> result = new List<TileScript>();
        for (int i = 0; i < size; i++) 
        {
            TileScript tmp = tileList.Find(x =>
            !result.Contains(x)
            && MainSceneManager.Instance.tileChecker.FindTilesInRange(x, 1).Count <= 3);

            Debug.Log(tmp.gameObject.GetInstanceID());

            if(tmp != null)
            {
                result.Add(tmp);
            }
        }

        return result;
    }
}
