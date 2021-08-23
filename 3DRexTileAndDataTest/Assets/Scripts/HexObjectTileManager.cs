using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexObjectTileManager : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    public void GenerateObjects(int width, int height)
    {
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
                    if (TileMapData.TileDatas[i,j].type == TileType.Plain) // 오브젝트 배치 불가능한 지형인지 체크
                    {
                        Debug.Log(TileMapData.TileDatas[i, j].type);
                        GameObject temp = Instantiate(randObj, this.gameObject.transform);
                        temp.transform.position = tilePos;
                    }
                }

                tilePos.x += 1;
            }
            tilePos.z += 0.875f;
        }
    }
}
