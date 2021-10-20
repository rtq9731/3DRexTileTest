using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObjectTileManager : LoadingObj
{

    [SerializeField] GameObject[] jungleGroundObjSet;
    [SerializeField] GameObject[] plainGroundObjSet;
    [SerializeField] GameObject[] MountainGroundObjSet;

    GameObject[] objects;

    private void Awake()
    {
        start = x => { x = "구름과 오브젝트 생성 중..."; };
        finish = x => { x = "구름과 오브젝트 생성 완료!"; };
    }

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
                TileScript curTile = TileMapData.Instance.GetTile(objNum);

                PlayerScript player = MainSceneManager.Instance.GetPlayer();

                if(!player.OwningTiles.Contains(curTile))
                {
                    if (MainSceneManager.Instance.tileChecker.FindTilesInRange(curTile, 1).Find(x => x.Owner == player) == null)
                    {
                        MainSceneManager.Instance.fogOfWarManager.GenerateCloudOnTile(curTile);
                    }
                }

                GameObject randObj = objects[UnityEngine.Random.Range(1, objects.Length)];

                if (randObj != null)
                {
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

        MainSceneManager.Instance.StartGame();
    }
}
