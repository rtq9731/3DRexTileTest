using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance = null;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public TileInfoScript InfoPanel;

    [SerializeField] public GameObject tileVcam;
    [SerializeField] public TileChecker tileChecker;
    [SerializeField] public UITopBar uiTopBar;

    public float TileZInterval = 0.875f;
    public float TileXInterval = 1f;
    public uint turnCnt = 0;

    public string PlayerName = "COCONUT";

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    private void Start()
    {
        InfoPanel = FindObjectOfType<TileInfoScript>();
        UIStackManager.RemoveUIOnTopWithNoTime();
    }

    public void CheckTurnFinish()
    {
        if(players.Find(x => x.IsTurnFinish == false))
        {
            return;
        }
        else
        {
            players.ForEach(x => x.StartNewTurn());
        }
    }

    public void StartGame()
    {
        foreach (var item in players)
        {
            item.AddTile(TileMapData.Instance.GetRandTile());
        }
    }

    public void LoadedGame()
    {

    }
}
