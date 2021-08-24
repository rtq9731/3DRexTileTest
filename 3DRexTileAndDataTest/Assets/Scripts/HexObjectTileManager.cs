using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObjectTileManager : MonoBehaviour
{

    [SerializeField] GameObject[] jungleGroundObjSet;
    [SerializeField] GameObject[] plainGroundObjSet;
    [SerializeField] GameObject[] MountainGroundObjSet;

    GameObject[] objects;

    public void GenerateObjects(int width, int height, HexTilemapGenerator.GroundType groundType)
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

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject randObj = objects[Random.Range(1, objects.Length)];

                if (randObj != null)
                {
                    if (TileMapData.TileDatas[i,j].Data.type == TileType.Plain) // 오브젝트 배치 불가능한 지형인지 체크
                    {
                        GameObject temp = Instantiate(randObj, TileMapData.TileDatas[i, j].transform);

                        switch (temp.GetComponent<ObjScript>().objType)
                        {
                            case ObjType.Tree:
                                TileMapData.TileDatas[i, j].Data.SetDataToForest();
                                break;
                            case ObjType.Mountain:
                                TileMapData.TileDatas[i, j].Data.SetDataToMountain();
                                break;
                            case ObjType.Rock:
                                TileMapData.TileDatas[i, j].Data.SetDataToRock();
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }
    }
}
