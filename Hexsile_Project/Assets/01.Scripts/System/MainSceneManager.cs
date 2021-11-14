using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance = null;

    [Header("������ ����")]
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

    [Header("�ܺ� ������")]
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
    public string PlayerName = "COCONUT";

    PersonPlayer player = null;

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

    public void SetPlayer(PersonPlayer player)
    {
        this.player = player;
    }

    public PersonPlayer GetPlayer()
    {
        return player;
    }

    public void StartLoadedGame()
    {
        SaveData curSave = GameManager.Instance.LoadData();

        player.PlayerData = curSave.playerData;
        TileMapData.Instance.GetAllTiles().FindAll(x => player.PlayerData.TileNums.Contains(x.Data.tileNum)).ForEach(x => player.AddTile(x)); // �÷��̾� �� �����ֱ�
        AIManager.Instance.LoadStage(curSave); // AI�� �� �ٽ� �ֱ�

        mapSize = curSave.mapSize;
        stageCount = curSave.stageCount;

        if (btnReroll == null)
        {
            btnReroll = FindObjectOfType<BtnReroll>();
        }

        isRerolled = curSave.isRerolled;
        if (!isRerolled)
        {
            btnReroll.ActiveReroll();
            player.TurnFinishAction += btnReroll.RemoveReroll;
        }

        player.TurnFinishAction += CheckStageClear;
    }

    public void StartGame()
    { 
        isRerolled = false;
        AIManager.Instance.StartStage(mapSize);
        player.AddTile(TileMapData.Instance.GetTile(0)); // ������ �߾Ӷ��� �÷��̾

        if(btnReroll == null)
        {
            btnReroll = FindObjectOfType<BtnReroll>();
        }

        btnReroll.ActiveReroll();
        player.TurnFinishAction += btnReroll.RemoveReroll;
        player.TurnFinishAction += CheckStageClear;
        GameManager.Instance.SaveData();
    }

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
        
        if(stageCount % 2 == 0 && AIManager.Instance.aiPlayerCount < 6) // ¦�� ��° �������� �϶� && AI�� 6�� ���� ��
        {
            AIManager.Instance.aiPlayerCount++;
        }
        mapSize++;

        player.ResetPlayer();
        tilemapGenerator.GenerateNewTile();
    }

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

    private void CheckStageClear()
    {
        if(AIManager.Instance.aiPlayers.Find(x => x.OwningTiles.Count >= 1) == null)
        {
            ClearStage();
        }
    }

    public void LoadGame()
    {

    }
}
