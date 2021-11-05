using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainPlayerData : CommonPlayerData
{
    [SerializeField] private int turnCnt = 0;
    [SerializeField] private List<MissileData> missileInMaking = new List<MissileData>();
    [SerializeField] private List<MissileData> missileReadyToShoot = new List<MissileData>();

    [SerializeField] private int researchFinishTurn = 0;

    [SerializeField] private List<int> unlockedWarheadIdx = new List<int>();
    [SerializeField] private List<int> unlockedEngineIdx = new List<int>();
    [SerializeField] private List<int> unlockedBodyIdx = new List<int>();
    [SerializeField] private List<int> researchedBodyResearch = new List<int>();
    [SerializeField] private List<int> researchedEngineResearch = new List<int>();
    [SerializeField] private SkillTreeNode curResearchData = null;
    [SerializeField] private int resourceTank = 0;

    public int TurnCnt
    {
        get { return turnCnt; }
        set { turnCnt = value; }
    }

    public int ResourceTank
    {
        get{ return resourceTank; }
        set{ resourceTank = value; }
    }

    public List<MissileData> MissileInMaking
    {
        get { return missileInMaking; }
    }

    public List<MissileData> MissileReadyToShoot
    {
        get { return missileReadyToShoot; }
    }

    public int ResearchFinishTurn
    {
        get { return researchFinishTurn; }
        set { researchFinishTurn = value; }
    }

    public List<int> UnlockedWarheadIdx
    {
        get { return unlockedWarheadIdx; }
    }

    public List<int> UnlockedEngineIdx
    {
        get { return unlockedEngineIdx; }
    }

    public List<int> UnlockedBodyIdx
    {
        get { return unlockedBodyIdx; }
    }

    public List<int> ResearchedBodyResearch
    {
        get { return researchedBodyResearch; }
    }

    public List<int> ResearchedEngineResearch
    {
        get { return researchedEngineResearch; }
    }

    public SkillTreeNode CurResearchData
    {
        get { return curResearchData; }
        set { curResearchData = value; }
    }

}
