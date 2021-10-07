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

    public void Fire(TileScript targetTile) // TODO : �̻��� ������Ʈ Ǯ���� �ϳ� �̾ƿͼ� ��� �����.
    {
        player.missileReadyToShoot.Remove(this);
    }
    
}
