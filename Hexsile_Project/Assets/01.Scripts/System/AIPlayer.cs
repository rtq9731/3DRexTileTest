using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerScript
{
    [SerializeField] AIData data = new AIData();

    public AIData Data
    {
        get { return data; } set { data = value; }
    }

    public override List<TileScript> OwningTiles {
        get { return data.OwningTiles; } 
        set { data.OwningTiles = value; }
    }

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
    }

    public void CheckIsDie()
    {
        data.OwningTiles = data.OwningTiles.Where(item => item != null).ToList();
        if (data.OwningTiles.Count < 1)
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
        data = new AIData();
        TurnFinishAction = () => { };
    }
}
