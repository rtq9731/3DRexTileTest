using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObjectTileManager : MonoBehaviour
{

    [SerializeField] GameObject[] jungleGroundObjSet;
    [SerializeField] GameObject[] plainGroundObjSet;
    [SerializeField] GameObject[] MountainGroundObjSet;

    GameObject[] objects;
    public void LoadAllTileObj()
    {
        List<ObjScript> objs = new List<ObjScript>();

        foreach (var item in plainGroundObjSet)
        {
            objs.Add(item.GetComponent<ObjScript>());
        }

        objs.Distinct();
        TileMapData.Instance.GetAllTiles().FindAll(x => x.Data.type != (TileType.Plain | TileType.Ocean | TileType.Lake)).ForEach(x => MakeObjOnTile(x, x.Data.type));
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
            if (curTile.Data.type == TileType.Plain) // ������Ʈ ��ġ �Ұ����� �������� üũ
            {
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
    }

    private void MakeObjOnTile(TileScript curTile, TileType type)
    {

    }
}
