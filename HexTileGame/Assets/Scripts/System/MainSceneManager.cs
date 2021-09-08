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

    public float TileZInterval = 0.875f;
    public float TileXInterval = 1f;

    public string PlayerName = "COCONUT";

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    private void Start()
    {
        InfoPanel = FindObjectOfType<TileInfoScript>();
        UIStackManager.RemoveUIOnTopWithNoTime();
    }

    public void StartGame()
    {
        foreach (var item in players)
        {
            Debug.Log(item);

#if UNITY_EDITOR
            TileScript temp = TileMapData.Instance.GetRandTile();
            item.AddTile(temp);
            tileChecker.FindTilesInRange(temp, 2).ForEach(x => Debug.Log(x.Data.type));
            return;
#endif
            item.AddTile(TileMapData.Instance.GetRandTile());
        }
    }

}
