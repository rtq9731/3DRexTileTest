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

        Vector3 tilePos = Vector3.zero;
        tilePos.z = -height / 2;
        for (int i = 0; i < height; i++)
        {
            tilePos.x = (i % 2 == 0) ? -width / 2 : -width / 2 + 0.5f;
            for (int j = 0; j < width; j++)
            {
                GameObject randObj = objects[Random.Range(1, objects.Length)];

                if (randObj != null)
                {
                    if (TileMapData.TileDatas[i,j].Data.type == TileType.Plain) // ������Ʈ ��ġ �Ұ����� �������� üũ
                    {
                        GameObject temp = Instantiate(randObj, TileMapData.TileDatas[i, j].transform);
                        temp.transform.position = tilePos;
                    }
                }

                tilePos.x += 1;
            }
            tilePos.z += 0.875f;
        }
    }
}
