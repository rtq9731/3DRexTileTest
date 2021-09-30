using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, ITurnFinishObj
{
    [SerializeField] protected string myName = "NULL";
    public Color playerColor;

    public string MyName
    {
        get { return myName; }
    }

    public List<TileScript> owningTiles = new List<TileScript>();
    public List<MissileData> missiles = new List<MissileData>();

    bool isTurnFinish = false;
    public bool IsTurnFinish { get { return isTurnFinish; } }

    int resouceTank = 0;
    public int ResourceTank { get { return resouceTank; } }

    protected void Start()
    {
        MainSceneManager.Instance.Players.Add(this);
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    public void FinishTurn()
    {
    }

    public void StartNewTurn()
    {
        isTurnFinish = false;
    }

    public void AddResource(int resource)
    {
        resouceTank += resource;
        MainSceneManager.Instance.uiTopBar.UpdateTexts();
    }

    public void TurnFinish()
    {
        if (isTurnFinish)
        {
            return;
        }
        else
        {
            isTurnFinish = true;

            owningTiles.ForEach(x => x.TurnFinish());
            MainSceneManager.Instance.CheckTurnFinish();
        }
    }

    public void AddTile(TileScript tile)
    {
        tile.ChangeOwner(this);
        owningTiles.Add(tile);
    }
}
