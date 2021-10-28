using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerScript
{
    private void Start()
    {
        MainSceneManager.Instance.Players.Add(this);
        MainSceneManager.Instance.AIPlayers.Add(this);
    }

    public override void AddTile(TileScript tile)
    {
        if (tile.Owner == MainSceneManager.Instance.GetPlayer() || tile.Data.type == (TileType.Lake | TileType.Ocean | TileType.None))
        {
            return;
        }

        tile.ChangeOwner(this);

        owningTiles.Remove(tile);
        owningTiles.Add(tile);
    }
}
