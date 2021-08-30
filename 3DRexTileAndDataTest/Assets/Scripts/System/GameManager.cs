using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    List<PlayerScript> players = new List<PlayerScript>();
    public List<PlayerScript> Players { get { return players; } }

    public void StartGame(params PlayerScript[] players)
    {
        foreach (var item in players)
        {
            TileMapData.Instance.GetRandTile(item);
        }
    }
}
