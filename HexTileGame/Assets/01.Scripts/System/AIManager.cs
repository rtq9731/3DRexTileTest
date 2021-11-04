using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] public int aiPlayerCount = 3;

    public List<AIPlayer> aiPlayers = new List<AIPlayer>();

    public static AIManager Instance = null;

    private int curTurnCnt = 0;

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

        MainSceneManager.Instance.GetPlayer().TurnFinishAction += CheckAndAttackPlayer;
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

    public void CheckAndAttackPlayer()
    {
        curTurnCnt++;
        Debug.Log(curTurnCnt);
        if (curTurnCnt >= 20)
        {
            if(curTurnCnt % 20 == 0)
            {
                AttackRandomTile();
            }
        }
        else if(curTurnCnt > 40)
        {
            if(curTurnCnt % 10 == 0)
            {
                AttackRandomTile();
            }
        }
        else if(curTurnCnt > 60)
        {
            if(curTurnCnt % 5 == 0)
            {
                AttackRandomTile();
            }
        }
    }

    private void AttackRandomTile()
    {
        var attackableAI = aiPlayers.FindAll(x => x.OwningTiles.Find(x => MainSceneManager.Instance.tileChecker.FindTilesInRange(x, 1).Find(x => x.Owner == MainSceneManager.Instance.GetPlayer()) != null) != null);
        foreach (var item in attackableAI)
        {
            List<TileScript> attackableTiles = new List<TileScript>();

            foreach (var itemTile in item.OwningTiles)
            {
                MainSceneManager.Instance.tileChecker.FindTilesInRange(itemTile, 1).FindAll(x => x.Owner == MainSceneManager.Instance.GetPlayer()).ForEach(x => attackableTiles.Add(x));
            }

            attackableTiles = attackableTiles.Distinct().ToList();

            MainSceneManager.Instance.missileManager.fireMissileFromStartToTarget(item.OwningTiles[0], 
                new MissileData(MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead, MissileTypes.MissileBody.Orign), 
                attackableTiles[Random.Range(0, attackableTiles.Count)], out GameObject missileObj);
        }
    }
}

enum EnemyGrade
{

}
