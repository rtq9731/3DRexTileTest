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

        int objNum = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                GameObject randObj = objects[Random.Range(1, objects.Length)];

                if (randObj != null)
                {
                    TileScript curTile = TileMapData.Instance.tileList[objNum];

                    if (curTile.Data.type == TileType.Plain) // 오브젝트 배치 불가능한 지형인지 체크
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

                objNum++;

            }
        }
    }
}
