using System;
using System.Linq;
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

    GameObject[] groundTiles;
    [SerializeField] List<GameObject> groundTileList = new List<GameObject>();

    private void Awake()
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
    }

    public void GenerateNewMap()
    {
        TileXInterval = MainSceneManager.Instance.TileXInterval;
        TileZInterval = MainSceneManager.Instance.TileZInterval;

        GenerateTiles(MainSceneManager.Instance.mapSize);
    }

    public void GenerateLoadedMap(List<TileData> Tiles)
    {
        foreach (var item in Tiles)
        {
            GameObject tile = null;
            switch (item.type)
            {
                default:
                    break;

                case TileType.Ocean:
                    tile = groundTileList.Find(x => x.GetComponent<TilePrefabScript>().prefabType == TilePrefabType.Ocean);
                    break;

                case TileType.Lake:
                    tile = groundTileList.Find(x => x.GetComponent<TilePrefabScript>().prefabType == TilePrefabType.Lake);
                    break;

                case TileType.DigSite:
                    tile = groundTileList.Find(x => x.GetComponent<TilePrefabScript>().prefabType == TilePrefabType.digSite);
                    break;

                case TileType.Forest:
                case TileType.Plain:
                case TileType.Mountain:
                    tile = groundTileList.Find(x => x.GetComponent<TilePrefabScript>().prefabType == TilePrefabType.Plain);
                    break;
            }

            GameObject temp = Instantiate(tile, item.Position, Quaternion.Euler(Vector3.zero), tileParent);
            TileScript tempScirpt = temp.GetComponent<TileScript>();
            tempScirpt.Data = item;

            TileMapData.Instance.SetTileData(tempScirpt);
        }

        GetComponent<HexObjectTileManager>().LoadAllTileObj();
    }

    public void GenerateNewTile()
    {
        Destroy(tileParent.gameObject);
        tileParent = Instantiate(parentObj, transform).transform;

        StartCoroutine(ReGenerateTiles());
    }

    public void GenerateNewTileWihtNoExtension()
    {
        Destroy(tileParent.gameObject);
        tileParent = Instantiate(parentObj, transform).transform;

        StartCoroutine(ReGenerateTiles());
    }

    IEnumerator ReGenerateTiles()
    {
        stageChangePanel.CallStageChangePanel(0.75f);
        yield return new WaitForSeconds(1);
        GenerateTiles(MainSceneManager.Instance.mapSize);
        MainSceneManager.Instance.stageClearPanel.CallStageClearPanel(() => { stageChangePanel.RemoveStageChangePanel(0.75f); });
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    private void GenerateTiles(int size)
    {
        List<Vector3> tilePos = MainSceneManager.Instance.tileChecker.MakeTilesPos(size);
        List<TileScript> Tiles = new List<TileScript>();

        for (int cnt = 0; cnt < tilePos.Count; cnt++)
        {
            GameObject temp = null;

            if (cnt == 0)
            {
                temp = Instantiate(groundTiles[0], tilePos[cnt], Quaternion.Euler(Vector3.zero), tileParent);
            }
            else
            {
                temp = Instantiate(groundTiles[UnityEngine.Random.Range(1, groundTiles.Length)], tilePos[cnt], Quaternion.Euler(Vector3.zero), tileParent);
            }

            TileScript tempScirpt = temp.GetComponent<TileScript>();
            tempScirpt.Data = new TileData();
            CheckGroundType(temp.GetComponent<TilePrefabScript>());

            switch (tempScirpt.Data.type)
            {
                case TileType.Ocean:
                    tempScirpt.Data.SetDataToOcean();
                    break;
                case TileType.Lake:
                    tempScirpt.Data.SetDataToLake();
                    break;
                case TileType.Forest:
                    tempScirpt.Data.SetDataToForest();
                    break;
                case TileType.DigSite:
                    tempScirpt.Data.SetDataToDigSite();
                    break;
                case TileType.Mountain:
                    tempScirpt.Data.SetDataToMountain();
                    break;

                case TileType.None:
                case TileType.Plain:
                default:
                    break;
            }

            tempScirpt.SetPosition(tilePos[cnt]);
            tempScirpt.Data.tileNum = cnt;
            TileMapData.Instance.SetTileData(tempScirpt);
        }

        List<TileScript> endTiles = TileMapData.Instance.GetAllTiles().FindAll(x => MainSceneManager.Instance.tileChecker.FindTilesInRange(x, 1).Count == 3);
        foreach (var item in endTiles)
        {
            GameObject temp = Instantiate(groundTiles[0], item.Data.Position, Quaternion.Euler(Vector3.zero), tileParent);
            TileScript tempScirpt = temp.GetComponent<TileScript>();
            TileMapData.Instance.SetTileData(tempScirpt);
            tempScirpt.SetPosition(item.Data.Position);
            tempScirpt.Data.tileNum = item.Data.tileNum;

            TileMapData.Instance.RemoveTileOnList(item);
            Destroy(item.gameObject);
            Destroy(item);
        } // 구석자리 타일은 무조건 배치 가능한 타일로 바꿔준다.

        GetComponent<HexObjectTileManager>().GenerateObjects(type);
    }

    private void CheckGroundType(TilePrefabScript tilePrefab)
    {
        TileScript tile = tilePrefab.GetComponent<TileScript>();
        switch (tilePrefab.prefabType)
        {
            case TilePrefabType.digSite:
                tile.Data.type = TileType.DigSite;
                break;

            case TilePrefabType.Ocean:
                tile.Data.type = TileType.Ocean;
                break;

            case TilePrefabType.Lake:
                tile.Data.type = TileType.Lake;
                break;

            case TilePrefabType.Plain:
            default:
                break;
        }
    }

}
