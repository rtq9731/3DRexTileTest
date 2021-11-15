using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerScript
{
    AIData data = new AIData();

    private void Start()
    {
        TurnFinishAction += CheckIsDie;
    }

    public AIData Data
    {
        get { return data; } set { data = value; }
    }

    public override List<TileScript> OwningTiles => data.OwningTiles;

    public override string MyName => data.PlayerName;

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
            data.IsGameOver = true;
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

    public override void ResetPlayer()
    {
        data.OwningTiles.Clear();
        data.IsGameOver = false;
    }

    public override void RemoveTile(TileScript tile)
    {
        data.OwningTiles.Remove(tile);

        if(OwningTiles.Count < 1)
        {
            data.IsGameOver = true;
        }
    }
}
