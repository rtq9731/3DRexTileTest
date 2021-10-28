using System.Collections.Generic;
using UnityEngine;

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

    [Header("About Tile")]
    public float TileZInterval = 0.875f;
    public float TileXInterval = 1f;
    public uint turnCnt = 0;

    [Header("About Player")]
    public string PlayerName = "COCONUT";

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    [SerializeField] PersonPlayer player = null;

    public PlayerScript GetPlayer()
    {
        return player;
    }

    public bool CheckTurnFinish()
    {
        if(players.Find(x => x.IsTurnFinish == false))
        {
            return false;
        }
        else
        {
            players.ForEach(x => x.StartNewTurn()); // ���� ������ �ѱ�鼭 True ��ȯ
            turnCnt++;
            uiTopBar.UpdateTexts();

            return true;
        }
    }

    public void StartGame()
    {
        foreach (var item in players)
        {
            TileScript tile = TileMapData.Instance.GetRandTile();
            if(item == player)
            {
                item.AddTile(TileMapData.Instance.GetTile(0));
                fogOfWarManager.RemoveCloudOnTile(TileMapData.Instance.GetTile(0));
                continue;
            }

            if (tile != null)
            {
                item.AddTile(tile);
            }
        }
    }

    public void LoadedGame()
    {

    }
}
