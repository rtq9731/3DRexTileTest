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

    [Header("About Tile")]
    public float TileZInterval = 0.875f;
    public float TileXInterval = 1f;
    public uint turnCnt = 0;

    [Header("About Player")]
    public string PlayerName = "COCONUT";
    public int AIPlayerCount = 3;

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    public List<AIPlayer> AIPlayers = new List<AIPlayer>();

    [SerializeField] PersonPlayer player = null;

    public PersonPlayer GetPlayer()
    {
        return player;
    }

    public void StartGame()
    {
        TileMapData.Instance.GetAllTiles().ForEach(x => x.ChangeOwner(null));

        foreach (var item in players)
        {
            if(item == player)
            {
                item.AddTile(TileMapData.Instance.GetTile(0));
                fogOfWarManager.RemoveCloudOnTile(TileMapData.Instance.GetTile(0));
                continue;
            }

            if (AIPlayers.FindAll(x => x.OwningTiles.Count >= 1).Count >= AIPlayerCount)
            {
                return;
            }

            List<TileScript> tiles = TileMapData.Instance.GetEndTile(6);
            tiles = tiles.FindAll(x => x.Owner == null);
            item.gameObject.SetActive(true);
            item.AddTile(tiles[Random.Range(0, tiles.Count)]);
        } 
    }

    public void LoadedGame()
    {

    }
}
