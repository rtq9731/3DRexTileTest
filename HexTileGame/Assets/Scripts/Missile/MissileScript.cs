using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript
{
    MissileData data = null;
    PlayerScript player = null;

    public void Start()
    {
        MainSceneManager.Instance.Players.Find(x => x.MyName == MainSceneManager.Instance.PlayerName);
        player.missileInMaking.Add(this);

        player.TurnFinishAction += () => turnFinishAct();
    }

    public void turnFinishAct()
    {
        this.data.TurnForMissileReady--;
        if(data.TurnForMissileReady == 0)
        {
            player.missileInMaking.Remove(this);
            player.missileReadyToShoot.Add(this);
        }
    }

    public void Fire(TileScript targetTile) // TODO : 미사일 오브젝트 풀에서 하나 뽑아와서 쏘도록 만드셈.
    {
        player.missileReadyToShoot.Remove(this);
    }
    
}
