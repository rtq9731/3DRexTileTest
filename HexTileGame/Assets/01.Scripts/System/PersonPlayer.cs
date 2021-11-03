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

    private SkillTreeNode curResearchData = null;
    public SkillTreeNode CurResearchData
    {
        get { return curResearchData; }
        set
        {
            switch (value.Type)
            {
                case ResearchType.Warhead:
                    if (unlockedEngineIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                case ResearchType.Engine:
                    if (researchedEngineResearch.Contains(value.Idx) || unlockedEngineIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                case ResearchType.Body:
                    if (researchedBodyResearch.Contains(value.Idx) || unlockedBodyIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                default:
                    break;
            }

            curResearchData = value;
            researchFinishTurn = curResearchData.TrunForResearch;

            TurnFinishAction -= ResearchOnTurnFinish;
            TurnFinishAction += ResearchOnTurnFinish;
            MainSceneManager.Instance.curResearchPanel.UpdateTexts(this);
        }
    }

    private List<int> unlockedWarheadIdx = new List<int>();
    public List<int> UnlockedWarheadIdx
    {
        get { return unlockedWarheadIdx; }
    }

    private List<int> unlockedEngineIdx = new List<int>();
    public List<int> UnlockedEngineIdx
    {
        get { return unlockedEngineIdx; }
    }

    private List<int> unlockedBodyIdx = new List<int>();
    public List<int> UnlockedBodyIdx
    {
        get { return unlockedBodyIdx; }
    }

    private List<int> researchedBodyResearch = new List<int>();
    public List<int> ResearchedBodyResearch
    {
        get { return researchedBodyResearch; }
    }

    private List<int> researchedEngineResearch = new List<int>();
    public List<int> ResearchedEngineResearch
    {
        get { return researchedEngineResearch; }
    }

    public override void ResetPlayer()
    {
        unlockedEngineIdx.Clear();
        unlockedWarheadIdx.Clear();
        unlockedBodyIdx.Clear();
        researchedBodyResearch.Clear();
        researchedEngineResearch.Clear();
        missileReadyToShoot.Clear();
        missileInMaking.Clear();
        owningTiles.Clear();

        curResearchData = null;

        resouceTank = 0;

        unlockedEngineIdx.Add(0); // 기본 연구는 완료 후 시작
        unlockedWarheadIdx.Add(0);
        unlockedBodyIdx.Add(0);

        TurnFinishAction = () => { }; // 액션 초기화

        TurnFinishAction += () => { MainSceneManager.Instance.turnCnt++; };

        TurnFinishAction += () => {
            missileInMaking.ForEach(x => x.TurnForMissileReady--);
            missileInMaking.FindAll(x => x.TurnForMissileReady <= 0).ForEach(x => {
                missileInMaking.Remove(x);
                missileReadyToShoot.Add(x);
            });
        };
    }

    private void ResearchOnTurnFinish()
    {
        if (curResearchData == null)
        {
            return;
        }

        researchFinishTurn--;
        if (researchFinishTurn <= 0)
        {
            switch (curResearchData.Type)
            {

                case ResearchType.Warhead:
                    unlockedWarheadIdx.Add(curResearchData.ResearchThingIdx);
                    break;
                case ResearchType.Engine:

                    if (curResearchData.ResearchThingIdx != -1)
                    {
                        unlockedEngineIdx.Add(curResearchData.ResearchThingIdx);
                    }

                    ResearchedEngineResearch.Add(curResearchData.Idx);
                    break;
                case ResearchType.Body:
                    if (curResearchData.ResearchThingIdx != -1)
                    {
                        unlockedBodyIdx.Add(curResearchData.ResearchThingIdx);
                    }

                    ResearchedBodyResearch.Add(curResearchData.Idx);
                    break;
                default:
                    break;
            }

            curResearchData = null;
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
        owningTiles.Remove(tile);
        owningTiles.Add(tile);
    }

    public override void TurnFinish()
    {
        TurnFinishAction();
        owningTiles.ForEach(x => x.TurnFinish());
    }

    private void Start()
    {
        unlockedEngineIdx.Add(0); // 기본 연구는 완료 후 시작
        unlockedWarheadIdx.Add(0);
        unlockedBodyIdx.Add(0);

        MainSceneManager.Instance.SetPlayer(this);
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
        MainSceneManager.Instance.PlayerName = playerName;
        playerColor = color;
        myName = playerName;
    }

}
