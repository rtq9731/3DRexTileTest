using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] public int aiPlayerCount = 3;

    public List<AIPlayer> aiPlayers = new List<AIPlayer>();

    public static AIManager Instance = null;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        aiPlayers = GetComponentsInChildren<AIPlayer>().ToList();
    }

    public void StartStage(int mapSize)
    {

        foreach (var item in aiPlayers) // 초기에 구석자리 땅 주는 부분
        {

            if (aiPlayers.FindAll(x => x.OwningTiles.Count >= 1).Count >= aiPlayerCount)
            {
                break;
            }

            List<TileScript> tiles = TileMapData.Instance.GetEndTile(6);
            tiles = tiles.FindAll(x => x.Owner == null);
            item.gameObject.SetActive(true);
            item.AddTile(tiles[Random.Range(0, tiles.Count)]);
        }

        var onlineAIPlayers = from result in aiPlayers
                              where result.OwningTiles.Count >= 1
                              select result;

        foreach (var item in onlineAIPlayers) // 구석자리 땅이 모두 배분 된 후 나머지 땅을 AI들끼리 나눈다.
        {
            MainSceneManager.Instance.tileChecker.FindTilesInRange(item.OwningTiles[0], mapSize / 2 + 1).ForEach(x =>
            {
                item.AddTile(x);
            });
        }
    }

    public void AIFireMissile()
    {
        
    }

    public void GetNewStage()
    {

    }
}

enum EnemyGrade
{

}
