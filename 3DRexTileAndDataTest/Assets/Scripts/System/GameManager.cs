using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public TileInfoScript InfoPanel;
    [SerializeField] public GameObject tileVcam;

    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    private void Awake()
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
