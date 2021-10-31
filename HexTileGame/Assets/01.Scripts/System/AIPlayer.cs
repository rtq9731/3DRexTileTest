using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerScript
{
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    private void Start()
    {
        MainSceneManager.Instance.Players.Add(this);
        MainSceneManager.Instance.AIPlayers.Add(this);
        TurnFinishAction += CheckIsDie;
        TurnFinishAction += CheckOtherAIStat;
    }

    public override void AddTile(TileScript tile)
    {
        if (tile.Owner == MainSceneManager.Instance.GetPlayer() ||
            tile.Data.type == TileType.Lake ||
            tile.Data.type == TileType.Ocean ||
            tile.Data.type == TileType.None)
        {
            return;
        }

        tile.ChangeOwner(this);

        owningTiles.Remove(tile);
        owningTiles.Add(tile);
    }

    private void CheckIsDie()
    {
        if(owningTiles.Count < 1)
        {
            isDead = true;
        }
    }

    public void CheckOtherAIStat()
    {
        if(MainSceneManager.Instance.AIPlayers.Find(x => !x.isDead) == null)
        {
            // 스테이지 끝내는 메서드 필요함
        }
    }
}
