using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObjectTileManager : MonoBehaviour
{

    [SerializeField] GameObject[] jungleGroundObjSet = null;
    [SerializeField] GameObject[] plainGroundObjSet = null;
    [SerializeField] GameObject[] MountainGroundObjSet = null;

    GameObject[] objects;
    List<ObjScript> objs = new List<ObjScript>();
    public void LoadAllTileObj()
    {
        objs = new List<ObjScript>();

        foreach (var item in plainGroundObjSet)
        {
            if (item != null)
                objs.Add(item.GetComponent<ObjScript>());
        }

        objs.Distinct();
        TileMapData.Instance.GetAllTiles().FindAll(x =>
        x.Data.type != TileType.Plain ||
        x.Data.type != TileType.Ocean ||
        x.Data.type != TileType.Lake).
        ForEach(x => LoadObjOnTile(x));

        MainSceneManager.Instance.StartLoadedGame();
    }

    public void GenerateObjects(HexTilemapGenerator.GroundType groundType)
    {
        switch (groundType)
        {
            case HexTilemapGenerator.GroundType.Jungle:
                objects = jungleGroundObjSet;
                break;
            case HexTilemapGenerator.GroundType.Plain:
                objects = plainGroundObjSet;
                break;
            case HexTilemapGenerator.GroundType.Mountain:
                objects = MountainGroundObjSet;
                break;
            default:
                break;
        }

        List<TileScript> tiles = TileMapData.Instance.GetAllTiles();

        for (int i = 0; i < tiles.Count; i++)
        {
            TileScript curTile = tiles[i];
            GameObject randObj = objects[UnityEngine.Random.Range(1, objects.Length)];

            MakeObjOnTile(curTile, randObj);
        }

        MainSceneManager.Instance.StartGame();
    }

    private void MakeObjOnTile(TileScript curTile, GameObject randObj)
    {
        MainSceneManager.Instance.fogOfWarManager.GenerateCloudOnTile(curTile);

        if (randObj != null)
        {
            if (curTile.Data.type != TileType.Plain)
                return;

            GameObject temp = Instantiate(randObj, curTile.transform);

            switch (temp.GetComponent<ObjScript>().objType)
            {
                case ObjType.Tree:
                    curTile.Data.SetDataToForest();
                    break;
                case ObjType.Mountain:
                    curTile.Data.SetDataToMountain();
                    break;
                case ObjType.Rock:
                    curTile.Data.SetDataToRock();
                    break;
                default:
                    break;
            }
        }
    }

    private void LoadObjOnTile(TileScript tile)
    {
        switch (tile.Data.type)
        {
            case TileType.None:
            case TileType.Ocean:
            case TileType.Lake:
            case TileType.Plain:
            case TileType.DigSite:
                MakeObjOnTile(tile, null);
                break;
            case TileType.Forest:
                Instantiate(objs.Find(x => x.objType == ObjType.Tree).gameObject, tile.transform);
                break;
            case TileType.Mountain:
                Instantiate(objs.Find(x => x.objType == ObjType.Mountain).gameObject, tile.transform);
                break;
            default:
                break;
        }
    }
}
