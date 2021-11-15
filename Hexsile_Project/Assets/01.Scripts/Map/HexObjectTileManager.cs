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

        Debug.Log(plainGroundObjSet.Length);
        foreach (var item in plainGroundObjSet)
        {
            if(item != null)
            objs.Add(item.GetComponent<ObjScript>());
        }

        objs.Distinct();
        TileMapData.Instance.GetAllTiles().FindAll(x => x.Data.type != (TileType.Plain | TileType.Ocean | TileType.Lake)).ForEach(x => MakeObjOnTile(x, x.Data.type));

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
        Debug.Log(randObj);
        MainSceneManager.Instance.fogOfWarManager.GenerateCloudOnTile(curTile);

        if (randObj != null)
        {
            GameObject temp = Instantiate(randObj, curTile.transform);


            if (curTile.Data.type == TileType.Plain)
                return;

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

    private void MakeObjOnTile(TileScript curTile, TileType type)
    {
        Debug.Log(type);
        switch (type)
        {
            case TileType.None:
            case TileType.Ocean:
            case TileType.Lake:
            case TileType.Plain:
            case TileType.DigSite:
                MakeObjOnTile(curTile, null);
                break;
            case TileType.Forest:
                MakeObjOnTile(curTile, objs.Find(x => x.objType == ObjType.Tree).gameObject);
                break;
            case TileType.Mountain:
                MakeObjOnTile(curTile, objs.Find(x => x.objType == ObjType.Mountain).gameObject);
                break;
            default:
                break;
        }
    }
}
