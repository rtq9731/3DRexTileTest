using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } set { players = value; } }

    public void StartGame()
    {
        foreach (var item in players)
        {
            item.AddTile(TileMapData.Instance.GetRandTile());
        }
    }
}
