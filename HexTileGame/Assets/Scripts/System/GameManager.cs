using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
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
            item.AddTile(TileMapData.Instance.GetRandTile());
        }
    }
}