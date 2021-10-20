using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerScript
{
    [SerializeField] int tilePrice = 20;

    private void Update()
    {
        if (isTurnFinish)
            return;
        
        if(resouceTank > tilePrice)
        {
            BuyTile();
        }
        else
        {
            TurnFinish();
        }
    }

    private void BuyTile()
    {
        TileScript buyTile = null;

        foreach (var item in owningTiles)
        {
            buyTile = MainSceneManager.Instance.tileChecker.FindTilesInRange(item, 1).Find(x => isCanBuy(x));
        }

        buyTile.BuyTile(this);
    }

    private bool isCanBuy(TileScript tile)
    {
        return tile.Owner == null
            && tile.Data.type != TileType.Ocean
            && tile.Data.type != TileType.Lake;
    }
}
