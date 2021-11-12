using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HexTilemapGenerator : MonoBehaviour
{
    Stack<Action> actionStack = new Stack<Action>();

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

    public void GenerateNewMap()
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

        GenerateTiles(MainSceneManager.Instance.mapSize);
    }

    public void GenerateLoadedMap()
    {

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
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
        yield return new WaitForSeconds(1);
        stageChangePanel.RemoveStageChangePanel(0.75f);
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
            CheckGroundType(tempScirpt);
            TileMapData.Instance.SetTileData(tempScirpt);
            tempScirpt.SetPosition(tilePos[cnt]);
            tempScirpt.Data.tileNum = cnt;

            Tiles.Add(tempScirpt);
        }

        List<TileScript> endTiles = Tiles.FindAll(x => MainSceneManager.Instance.tileChecker.FindTilesInRange(x, 1).Count == 3);
        foreach (var item in endTiles)
        {
            actionStack.Push(() =>
            {
                GameObject temp = Instantiate(groundTiles[0], item.Data.Position, Quaternion.Euler(Vector3.zero), tileParent);
                TileScript tempScirpt = temp.GetComponent<TileScript>();
                TileMapData.Instance.SetTileData(tempScirpt);
                tempScirpt.SetPosition(item.Data.Position);
                tempScirpt.Data.tileNum = item.Data.tileNum;

                TileMapData.Instance.RemoveTileOnList(item);
                Destroy(item.gameObject);
            });
        }

        while (actionStack.Count >= 1)
        {
            actionStack.Pop()();
        }

        GetComponent<HexObjectTileManager>().GenerateObjects(size, type);
    }

    private void CheckGroundType(TileScript obj)
    {
        string objName = obj.gameObject.name;
        Debug.Log(objName);
        if (objName.Contains("Lake"))
        {
            obj.Data.type = TileType.Lake;
        }
        else if(objName.Contains("Ocean"))
        {
            obj.Data.type = TileType.Ocean;
        }
        else if(objName.Contains("DigSite"))
        {
            obj.Data.type = TileType.DigSite;
        }
    }

    private void LoadMap()
    {
        List<TileScript> tiles = TileMapData.Instance.GetAllTiles();
    }

}
