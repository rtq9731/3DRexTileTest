using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexTilemapGenerator : MonoBehaviour
{
    public enum GroundType
    {
        None,
        Jungle,
        Plain,
        Mountain
    }

    Func<string> start = () => { return "오브젝트 생성중.."; };
    Func<string> finish = () => { return "오브젝트 생성 완료!"; };

    float TileXInterval = 1f;
    float TileZInterval = 0.875f;

    [SerializeField] GroundType type;

    [SerializeField] StageChange stageChangePanel = null;

    [SerializeField] GameObject[] jungleGroundTileSet;
    [SerializeField] GameObject[] plainGroundTileSet;
    [SerializeField] GameObject[] MountainGroundTileSet;

    [SerializeField] GameObject beachTile;

    [SerializeField] GameObject parentObj = null;
    [SerializeField] Transform tileParent = null;

    int mapSize = 3;

    GameObject[] groundTiles;

    private void Start()
    {
        switch (type)
        {
            case GroundType.Jungle:
                groundTiles = jungleGroundTileSet;
                break;
            case GroundType.Plain:
                groundTiles = plainGroundTileSet;
                break;
            case GroundType.Mountain:
                groundTiles = MountainGroundTileSet;
                break;
            default:
                break;
        }

        TileXInterval = MainSceneManager.Instance.TileXInterval;
        TileZInterval = MainSceneManager.Instance.TileZInterval;

        GenerateTiles(3);
    }

    public void GenerateNewTile()
    {
        DOTween.CompleteAll();
        while(!UIStackManager.IsUIStackEmpty())
        {
            UIStackManager.RemoveUIOnTopWithNoTime();
        }

        MainSceneManager.Instance.fogOfWarManager.ResetCloudList();
        TileMapData.Instance.ResetTileList();
        MainSceneManager.Instance.Players.ForEach(x => x.ResetPlayer());

        Destroy(tileParent.gameObject);
        tileParent = Instantiate(parentObj, transform).transform;

        mapSize++;
        StartCoroutine(ReGenerateTiles());
    }

    public void GenerateNewTileWihtNoExtension()
    {
        DOTween.CompleteAll();
        while (!UIStackManager.IsUIStackEmpty())
        {
            UIStackManager.RemoveUIOnTopWithNoTime();
        }

        MainSceneManager.Instance.fogOfWarManager.ResetCloudList();
        TileMapData.Instance.ResetTileList();
        MainSceneManager.Instance.Players.ForEach(x => x.ResetPlayer());

        Destroy(tileParent.gameObject);
        tileParent = Instantiate(parentObj, transform).transform;

        StartCoroutine(ReGenerateTiles());
    }

    IEnumerator ReGenerateTiles()
    {
        stageChangePanel.CallStageChangePanel(0.75f);
        yield return new WaitForSeconds(1);
        GenerateTiles(mapSize);
        stageChangePanel.RemoveStageChangePanel(0.75f);
    }

    private void GenerateTiles(int size)
    {
        List<Vector3> tiles = MainSceneManager.Instance.tileChecker.MakeTilesPos(size);

        for (int cnt = 0; cnt < tiles.Count; cnt++)
        {
            GameObject temp = null;

            if (cnt == 0)
            {
                temp = Instantiate(groundTiles[0], tiles[cnt], Quaternion.Euler(Vector3.zero), tileParent);
            }
            else
            {
                temp = Instantiate(groundTiles[UnityEngine.Random.Range(1, groundTiles.Length)], tiles[cnt], Quaternion.Euler(Vector3.zero), tileParent);
            }

            TileScript tempScirpt = temp.GetComponent<TileScript>();
            TileMapData.Instance.SetTileData(tempScirpt);
            tempScirpt.SetPosition(tiles[cnt]);
            tempScirpt.Data.tileNum = cnt;
        }

        GetComponent<HexObjectTileManager>().GenerateObjects(size, type);
    }

}
