using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapData : MonoBehaviour
{
    public static TileMapData Instance;

    public List<TileScript> tileList = new List<TileScript>();

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

    public void SetTileData(TileScript item)
    {
        tileList.Add(item);
    }
}
