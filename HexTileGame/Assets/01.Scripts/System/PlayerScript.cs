using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    protected List<PlayerScript> contactPlayers = new List<PlayerScript>();

    protected List<TileScript> tileInSight = new List<TileScript>();

    protected List<int> researchedEngineResearch = new List<int>();
    public List<int> ResearchedEngineResearch
    {
        get { return researchedEngineResearch; }
    }

    protected List<int> unlockedWarheadIdx = new List<int>();
    public List<int> UnlockedWarheadIdx
    {
        get { return unlockedWarheadIdx; }
    }

    protected List<int> unlockedEngineIdx = new List<int>();
    public List<int> UnlockedEngineIdx
    {
        get { return unlockedEngineIdx; }
    }

    [SerializeField] protected string myName = "NULL";
    public Color playerColor;

    public string MyName
    {
        get { return myName; }
    }

    public Action TurnFinishAction;

    protected List<TileScript> owningTiles = new List<TileScript>();
    public List<TileScript> OwningTiles
    {
        get { return owningTiles; }
    }

    protected List<MissileData> missileInMaking = new List<MissileData>();
    public  List<MissileData> MissileInMaking
    {
        get { return missileInMaking; }
    }

    protected List<MissileData> missileReadyToShoot = new List<MissileData>();
    public List<MissileData> MissileReadyToShoot
    {
        get { return missileReadyToShoot; }
    }

    protected int researchFinishTurn = 0;
    public int ResearchFinishTurn
    {
        get { return researchFinishTurn; }
    }

    protected SkillTreeNode curResearchData = null;
    public SkillTreeNode CurResearchData
    {
        get { return curResearchData; }
        set 
        {
            switch (value.Type)
            {
                case ResearchType.Warhead:
                    if (UnlockedWarheadIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                case ResearchType.Engine:
                    if (UnlockedEngineIdx.Contains(value.ResearchThingIdx))
                        return;
                    break;
                case ResearchType.Material:
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

    protected bool isTurnFinish = false;
    public bool IsTurnFinish { get { return isTurnFinish; } }

    protected int resouceTank = 0;
    public int ResourceTank { get { return resouceTank; } }

    protected void Awake()
    {
        TurnFinishAction = () => { }; // 액션 초기화
        TurnFinishAction += () => {
            missileInMaking.ForEach(x => x.TurnForMissileReady--);
            missileInMaking.FindAll(x => x.TurnForMissileReady <= 0).ForEach(x => {
                missileInMaking.Remove(x);
                missileReadyToShoot.Add(x);
                });
            };
    }

    protected void Start()
    {
        unlockedEngineIdx.Add(0); // 기본 연구는 완료 후 시작
        unlockedWarheadIdx.Add(0);

        MainSceneManager.Instance.Players.Add(this);
        MainSceneManager.Instance.uiTopBar.UpdateTexts();

        TurnFinishAction += () => contactPlayers.ForEach(x => x.owningTiles.ForEach(x => MainSceneManager.Instance.fogOfWarManager.RemoveCloudOnTile(x)));
    }

    public void StartNewTurn()
    {
        isTurnFinish = false;
    }

    public void AddResource(int resource)
    {
        resouceTank += resource;
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    public void TurnFinish()
    {
        if (isTurnFinish)
        {
            return;
        }
        else
        {
            isTurnFinish = true;

            TurnFinishAction();
            owningTiles.ForEach(x => x.TurnFinish());
            MainSceneManager.Instance.CheckTurnFinish();
        }
    }

    private void ResearchOnTurnFinish()
    {
        if (curResearchData == null)
        {
            return;
        }

        researchFinishTurn--;
        if(researchFinishTurn <= 0)
        {
            switch (curResearchData.Type)
            {

                case ResearchType.Warhead:
                    unlockedWarheadIdx.Add(curResearchData.ResearchThingIdx);
                    break;
                case ResearchType.Engine:

                    if(curResearchData.ResearchThingIdx != -1)
                    {
                        unlockedEngineIdx.Add(curResearchData.ResearchThingIdx);
                    }

                    ResearchedEngineResearch.Add(curResearchData.Idx);
                    break;
                case ResearchType.Material:
                    break;
                default:
                    break;
            }

            curResearchData = null;
        }
        MainSceneManager.Instance.curResearchPanel.UpdateTexts(this);
    }

    public void AddTile(TileScript tile)
    {
        if(tile.Owner != this)
        {
            tile.ChangeOwner(this);
        }

        if (MainSceneManager.Instance.GetPlayer() == this)
        {
            MainSceneManager.Instance.fogOfWarManager.RemoveCloudOnTile(tile);
            List<TileScript> tilesInRange = MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, 1);
            tilesInRange.ForEach(x => MainSceneManager.Instance.fogOfWarManager.RemoveCloudOnTile(x));

            var otherPlayers = from item in tilesInRange
                                            where item.Owner != this && item.Owner != null
                                            select item.Owner;
            if(otherPlayers.Count() >= 1)
            {
                foreach (var item in otherPlayers)
                {
                    if(!contactPlayers.Contains(item))
                    {
                        contactPlayers.Add(item);
                        item.owningTiles.ForEach(x => MainSceneManager.Instance.fogOfWarManager.RemoveCloudOnTile(x));

                        if(!item.contactPlayers.Contains(this))
                        {
                            item.contactPlayers.Add(this);
                        }    
                    }
                }
            }
        }

        // 중복 안되게 하기 위함.
        owningTiles.Remove(tile);
        owningTiles.Add(tile);
    }
}
