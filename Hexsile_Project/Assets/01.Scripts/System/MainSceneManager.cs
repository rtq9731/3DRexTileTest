using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance = null;

    [Header("데이터 관련")]
    [SerializeField] MissileWarhead missileWarhead;
    [SerializeField] MissileEngine missileEngine;
    [SerializeField] Body missileBody;
    [SerializeField] public TechTreeDatas techTreeDatas;
    
    public BodyData GetMissileBodyData(MissileTypes.MissileBody type)
    {
        return missileBody.dataList.Find(x => x.TYPE == type);
    }

    public BodyData GetMissileBodyByIdx(int idx)
    {
        return missileBody.dataArray[idx];
    }

    public MissileWarheadData GetWarheadData(MissileTypes.MissileWarheadType type)
    {
        return missileWarhead.dataList.Find(x => x.TYPE == type);
    }

    public MissileWarheadData GetWarheadByIdx(int idx)
    {
        return missileWarhead.dataArray[idx];
    }

    public MissileEngineData GetEngineData(MissileTypes.MissileEngineType type)
    {
        return missileEngine.dataList.Find(x => x.TYPE == type);
    }

    public MissileEngineData GetEngineDataByIdx(int idx)
    {
        return missileEngine.dataArray[idx];
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    [Header("외부 참조용")]
    [SerializeField] public TileInfoScript InfoPanel;
    [SerializeField] public MissileManager missileManager;
    [SerializeField] public FogOfWarManager fogOfWarManager;
    [SerializeField] public GameObject tileVcam;
    [SerializeField] public TileChecker tileChecker;
    [SerializeField] public UITopBar uiTopBar;
    [SerializeField] public MissileEffectPool effectPool;
    [SerializeField] public PanelResearchInput researchInputPanel;
    [SerializeField] public PanelCurrentResearch curResearchPanel;
    [SerializeField] public PanelMissileMaker missileMakerPanel;
    [SerializeField] public HexTilemapGenerator tileGenerator;

    [Header("About Tile")]
    public float TileZInterval = 0.875f;
    public float TileXInterval = 1f;
    public uint turnCnt = 0;
    public uint stageCount = 1;
    public int mapSize = 3;

    public bool isRerolled = false;

    private HexTilemapGenerator tilemapGenerator;
    private BtnReroll btnReroll = null;

    [Header("About Player")]
    [SerializeField] PersonPlayer player = null;

    private void Start()
    {
        tilemapGenerator = FindObjectOfType<HexTilemapGenerator>();

        if(GameManager.Instance.LoadData() != null)
        {
            tilemapGenerator.GenerateLoadedMap(GameManager.Instance.LoadData().tiles.ToList());
            return;
        }

        tilemapGenerator.GenerateNewMap();
    }

    /// <summary>
    /// 플레이어를 전달 받습니다
    /// </summary>
    /// <returns>플레이어 스크립트</returns>
    public PersonPlayer GetPlayer()
    {
        return player;
    }

    /// <summary>
    /// 저장되었던 게임을 로드해줍니다.
    /// </summary>
    public void StartLoadedGame()
    {
        SaveData curSave = GameManager.Instance.LoadData(); 

        player.PlayerData = curSave.playerData;
        player.PlayerData.PlayerName = GameManager.Instance.playerName;
        player.PlayerData.PlayerColor = GameManager.Instance.playerColor;

        TileMapData.Instance.GetAllTiles().FindAll(x => player.PlayerData.TileNums.Contains(x.Data.tileNum)).ForEach(x => player.AddTile(x)); // 플레이어 땅 돌려주기
        AIManager.Instance.LoadStage(curSave); // AI들 땅 다시 주기

        mapSize = curSave.mapSize;
        stageCount = curSave.stageCount;
        turnCnt = curSave.turnCnt;
        AIManager.Instance.aiPlayerCount = curSave.aiPlayerCount;

        if (btnReroll == null)
        {
            btnReroll = FindObjectOfType<BtnReroll>();
        }

        isRerolled = curSave.isRerolled;
        if (!isRerolled && turnCnt < 1)
        {
            btnReroll.ActiveReroll();
            player.TurnFinishAction += btnReroll.RemoveReroll;
        }

        player.TurnFinishAction += CheckStageClear;

        uiTopBar.UpdateTexts();
    }

    /// <summary>
    /// 스테이지를 시작합니다
    /// </summary>
    public void StartGame()
    {
        isRerolled = false;

        player.PlayerData.PlayerName = GameManager.Instance.playerName;
        player.PlayerData.PlayerColor = GameManager.Instance.playerColor;

        AIManager.Instance.StartStage(mapSize);
        player.AddTile(TileMapData.Instance.GetTile(0)); // 무조건 중앙땅은 플레이어꺼

        if(btnReroll == null)
        {
            btnReroll = FindObjectOfType<BtnReroll>();
        }

        btnReroll.ActiveReroll();
        player.TurnFinishAction += btnReroll.RemoveReroll;
        player.TurnFinishAction += CheckStageClear;
        GameManager.Instance.SaveData();

        uiTopBar.UpdateTexts();
    }

    /// <summary>
    /// 스테이지를 클리어하고 다음 스테이지로 넘어갑니다
    /// </summary>
    public void ClearStage()
    {
        DOTween.CompleteAll();
        while (!UIStackManager.IsUIStackEmpty())
        {
            UIStackManager.RemoveUIOnTopWithNoTime();
        }

        fogOfWarManager.ResetCloudList();
        TileMapData.Instance.ResetTileList();

        AIManager.Instance.aiPlayers.ForEach(x => x.ResetPlayer());

        turnCnt = 0;
        
        if(stageCount % 2 == 0 && AIManager.Instance.aiPlayerCount < 6) // 짝수 번째 스테이지 일때 && AI가 6개 밑일 때
        {
            AIManager.Instance.aiPlayerCount = 3 + (int)stageCount / 2;
        }
        mapSize++;
        stageCount++;

        player.ResetPlayer();
        tilemapGenerator.GenerateNewTile();
    }

    /// <summary>
    /// 새로운 맵을 생성합니다.
    /// </summary>
    public void RerollStage()
    {
        if(isRerolled)
        {
            return;
        }

        isRerolled = true;
        btnReroll.RemoveReroll();

        while (!UIStackManager.IsUIStackEmpty())
        {
            UIStackManager.RemoveUIOnTopWithNoTime();
        }

        fogOfWarManager.ResetCloudList();
        TileMapData.Instance.ResetTileList();

        AIManager.Instance.aiPlayers.ForEach(x => x.ResetPlayer());

        turnCnt = 0;
        player.ResetPlayer();
        tilemapGenerator.GenerateNewTileWihtNoExtension();
    }

    /// <summary>
    /// 스테이지 클리어를 체크합니다
    /// </summary>
    private void CheckStageClear()
    {
        if (AIManager.Instance.aiPlayers.Find(x => !x.Data.IsGameOver) == null)
        {
            ClearStage();
        }
    }
}
