using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public TileInfoScript InfoPanel;
    [SerializeField] public GameObject tileVcam;

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
            item.AddTile(TileMapData.Instance.GetRandTile());
        }
    }
}
