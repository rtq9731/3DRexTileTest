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
        curTurnCnt = 0;

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
            MainSceneManager.Instance.tileChecker.FindTilesInRange(item.OwningTiles[0], mapSize - 1).ForEach(x =>
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
        if (curTurnCnt <= 20)
        {
            if (curTurnCnt % 20 == 0)
            {
                AttackRandomTileByEachAI();
            }
        }
        else
        {
            if (curTurnCnt % 5 == 0)
            {
                AttackRandomTileByEachAI();
            }
        }
    }

    private void AttackRandomTileByEachAI()
    {
        List<AIPlayer> attackableAI = new List<AIPlayer>();

        List<AIPlayer> onlineAI = aiPlayers.FindAll(x => x.OwningTiles.Count >= 1);
        foreach (var item in onlineAI)
        {
            Debug.Log(item.MyName);

            if(item.CanAttack(out List<TileScript> attackableTiles))
            {
                attackableAI.Add(item);
            }
        }

        Debug.Log(attackableAI.Count);

        foreach (var item in attackableAI)
        {

            if (item.OwningTiles.Count >= 3)
            {
                for (int i = 0; i < item.OwningTiles.Count / 3; i++)
                {
                    aiRandAttack(item);
                }
            }
            else
            {
                aiRandAttack(item);
            }
        }
    }

    private void aiRandAttack(AIPlayer ai)
    {
        List<TileScript> attackableTiles = new List<TileScript>();

        foreach (var itemTile in ai.OwningTiles)
        {
            MainSceneManager.Instance.tileChecker.FindTilesInRange(itemTile, 1).FindAll(x => x.Owner == MainSceneManager.Instance.GetPlayer()).ForEach(x => attackableTiles.Add(x));
        }

        attackableTiles = attackableTiles.Distinct().ToList();

        MainSceneManager.Instance.missileManager.fireMissileFromStartToTarget(ai.OwningTiles[0],
            new MissileData(MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead, MissileTypes.MissileBody.Orign),
            attackableTiles[Random.Range(0, attackableTiles.Count)], out GameObject missileObj);
    }
}
