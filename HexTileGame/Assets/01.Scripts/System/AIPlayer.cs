using System.Linq;
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
        TurnFinishAction += CheckIsDie;
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

        data.OwningTiles.Remove(tile);
        data.OwningTiles.Add(tile);
    }

    private void CheckIsDie()
    {
        if(data.OwningTiles.Count < 1)
        {
            isDead = true;
        }
    }

    public bool CanAttack(out List<TileScript> attackAbleTiles)
    {
        List<TileScript> tiles = new List<TileScript>();
        foreach (var item in OwningTiles)
        {
            MainSceneManager.Instance.tileChecker.FindTilesInRange(item, 1).ForEach(x => tiles.Add(x));
        }

        attackAbleTiles = tiles.Distinct().ToList().FindAll(x => x.Owner == MainSceneManager.Instance.GetPlayer());

        return attackAbleTiles.Count >= 1 ? true : false;
    }
}
