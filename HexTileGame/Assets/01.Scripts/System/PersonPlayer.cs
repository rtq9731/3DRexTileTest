using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPlayer : PlayerScript
{
    [Header("플레이어 정보 입력")]
    [SerializeField] string playerName;
    [SerializeField] Color color;

    [SerializeField] new MainPlayerData playerData = new MainPlayerData();

    
    public List<MissileData> MissileInMaking
    {
        get { return playerData.MissileInMaking; }
    }
    public List<MissileData> MissileReadyToShoot
    {
        get { return playerData.MissileReadyToShoot; }
    }
    public int ResearchFinishTurn
    {
        get { return playerData.ResearchFinishTurn; }
        set { playerData.ResearchFinishTurn = value; }
    }
    public List<int> UnlockedWarheadIdx
    {
        get { return playerData.UnlockedWarheadIdx; }
    }
    public List<int> UnlockedEngineIdx
    {
        get { return playerData.UnlockedEngineIdx; }
    }
    public List<int> UnlockedBodyIdx
    {
        get { return playerData.UnlockedBodyIdx; }
    }
    public List<int> ResearchedBodyResearch
    {
        get { return playerData.ResearchedBodyResearch; }
    }
    public List<int> ResearchedEngineResearch
    {
        get { return playerData.ResearchedEngineResearch; }
    }
    public int ResourceTank
    {
        get { return playerData.ResourceTank; }
    }
    public override List<TileScript> OwningTiles
    {
        get { return playerData.OwningTiles; }
    }

    private void Awake()
    {
        TurnFinishAction += () => { MainSceneManager.Instance.turnCnt++; };

        TurnFinishAction += () => {
            playerData.MissileInMaking.ForEach(x => x.TurnForMissileReady--);
            playerData.MissileInMaking.FindAll(x => x.TurnForMissileReady <= 0).ForEach(x => {
                playerData.MissileInMaking.Remove(x);
                playerData.MissileReadyToShoot.Add(x);
            });
        };
    }

    private void Start()
    {
        playerData.UnlockedEngineIdx.Add(0); // 기본 연구는 완료 후 시작
        playerData.UnlockedWarheadIdx.Add(0);
        playerData.UnlockedBodyIdx.Add(0);

        MainSceneManager.Instance.SetPlayer(this);
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
        MainSceneManager.Instance.PlayerName = playerName;
        playerColor = color;
        myName = playerName;
    }

    public SkillTreeNode CurResearchData
    {
        get 
        {
            Debug.Log(playerData.CurResearchData);
            return playerData.CurResearchData; 
        }
        set
        {
            switch (value.Type)
            {
                case ResearchType.Warhead:
                    if (playerData.UnlockedEngineIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                case ResearchType.Engine:
                    if (playerData.ResearchedEngineResearch.Contains(value.Idx) || playerData.UnlockedEngineIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                case ResearchType.Body:
                    if (playerData.ResearchedBodyResearch.Contains(value.Idx) || playerData.UnlockedBodyIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                default:
                    break;
            }

            playerData.CurResearchData = value;
            playerData.ResearchFinishTurn = playerData.CurResearchData.TrunForResearch;

            TurnFinishAction -= ResearchOnTurnFinish;
            TurnFinishAction += ResearchOnTurnFinish;
            MainSceneManager.Instance.curResearchPanel.UpdateTexts(this);
        }
    }

    public override void ResetPlayer()
    {
        playerData = new MainPlayerData();

        playerData.CurResearchData = null;

        playerData.ResourceTank = 0;

        playerData.UnlockedEngineIdx.Add(0); // 기본 연구는 완료 후 시작
        playerData.UnlockedWarheadIdx.Add(0);
        playerData.UnlockedBodyIdx.Add(0);

        TurnFinishAction = () => { }; // 액션 초기화

        TurnFinishAction += () => { MainSceneManager.Instance.turnCnt++; };

        TurnFinishAction += () => {
            playerData.MissileInMaking.ForEach(x => x.TurnForMissileReady--);
            playerData.MissileInMaking.FindAll(x => x.TurnForMissileReady <= 0).ForEach(x => {
                playerData.MissileInMaking.Remove(x);
                playerData.MissileReadyToShoot.Add(x);
            });
        };
    }

    private void ResearchOnTurnFinish()
    {
        if (playerData.CurResearchData == null)
        {
            return;
        }

        playerData.ResearchFinishTurn--;
        if (playerData.ResearchFinishTurn <= 0)
        {
            switch (playerData.CurResearchData.Type)
            {

                case ResearchType.Warhead:
                    playerData.UnlockedWarheadIdx.Add(playerData.CurResearchData.ResearchThingIdx);
                    break;
                case ResearchType.Engine:

                    if (playerData.CurResearchData.ResearchThingIdx != -1)
                    {
                        playerData.UnlockedEngineIdx.Add(playerData.CurResearchData.ResearchThingIdx);
                    }

                    playerData.ResearchedEngineResearch.Add(playerData.CurResearchData.Idx);
                    break;
                case ResearchType.Body:
                    if (playerData.CurResearchData.ResearchThingIdx != -1)
                    {
                        playerData.UnlockedBodyIdx.Add(playerData.CurResearchData.ResearchThingIdx);
                    }

                    playerData.ResearchedBodyResearch.Add(playerData.CurResearchData.Idx);
                    break;
                default:
                    break;
            }

            playerData.CurResearchData = null;
        }
        MainSceneManager.Instance.curResearchPanel.UpdateTexts(this);
    }

    public override void AddTile(TileScript tile)
    {
        if (tile.Owner != this)
        {
            tile.ChangeOwner(this);
        }

        MainSceneManager.Instance.fogOfWarManager.RemoveCloudOnTile(tile);
        List<TileScript> tilesInRange = MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, 1);

        if (tile.Data.type == TileType.Mountain)
        {
            tilesInRange = MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, 2);
        }

        tilesInRange.ForEach(x =>
        {
            if (x.transform.GetComponentInChildren<CloudObject>() != null)
            {
                MainSceneManager.Instance.fogOfWarManager.RemoveCloudOnTile(x);
            }
        });

        // 중복 안되게 하기 위함.
        playerData.OwningTiles.Remove(tile);
        playerData.OwningTiles.Add(tile);
    }

    public override void TurnFinish()
    {
        TurnFinishAction();
        playerData.OwningTiles.ForEach(x => x.TurnFinish());
    }

    public void AddResource(int resource)
    {
        playerData.ResourceTank += resource;
        Debug.Log(resource);
        Debug.Log(playerData.ResourceTank);
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

}
