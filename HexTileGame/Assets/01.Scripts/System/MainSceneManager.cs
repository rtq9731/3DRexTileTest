using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance = null;

    [Header("데이터 관련")]
    [SerializeField] MissileWarhead missileWarhead;
    [SerializeField] MissileEngine missileEngine;
    [SerializeField] Body missileBody;
    [SerializeField] public TechTreeDatas techTreeDatas;
    [SerializeField] int rerollCount = 1;

    public bool CanReroll()
    {
        Debug.Log(rerollCount > 0);
        return rerollCount > 0;
    }

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
    public int mapSize;

    [Header("About Player")]
    public string PlayerName = "COCONUT";
    public int AIPlayerCount = 3;

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    public List<AIPlayer> AIPlayers = new List<AIPlayer>();

    PersonPlayer player = null;

    public void SetPlayer(PersonPlayer player)
    {
        this.player = player;
    }

    public PersonPlayer GetPlayer()
    {
        return player;
    }

    public void StartGame()
    {
        AIPlayers = FindObjectsOfType<AIPlayer>().ToList();

        foreach (var item in AIPlayers) // 초기에 구석자리 땅 주는 부분
        {

            if (AIPlayers.FindAll(x => x.OwningTiles.Count >= 1).Count >= AIPlayerCount)
            {
                break;
            }

            List<TileScript> tiles = TileMapData.Instance.GetEndTile(6);
            tiles = tiles.FindAll(x => x.Owner == null);
            item.gameObject.SetActive(true);
            item.AddTile(tiles[Random.Range(0, tiles.Count)]);
        }

        var onlineAIPlayers = from result in AIPlayers
                              where result.OwningTiles.Count >= 1
                              select result;
        Debug.Log(onlineAIPlayers.Count());

        foreach (var item in onlineAIPlayers) // 구석자리 땅이 모두 배분 된 후 나머지 땅을 AI들끼리 나눈다.
        {
            Debug.Log(mapSize / 2);
            tileChecker.FindTilesInRange(item.OwningTiles[0], mapSize / 2 + 1).ForEach(x =>
            {
                item.AddTile(x);
                });
        }

        player.AddTile(TileMapData.Instance.GetTile(0)); // 무조건 중앙땅은 플레이어꺼
        fogOfWarManager.RemoveCloudOnTile(TileMapData.Instance.GetTile(0)); 

        tileChecker.FindTilesInRange(player.OwningTiles[0], 1).ForEach(x => fogOfWarManager.RemoveCloudOnTile(x));
    }

    public void ClearStage()
    {

    }

    public void LoadGame()
    {

    }
}
