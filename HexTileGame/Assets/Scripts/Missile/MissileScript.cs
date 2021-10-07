using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript
{
    MissileData data = null;
    PlayerScript player = null;

    private PanelMissileQueueElement linkedUI;
    public PanelMissileQueueElement LinkedUI
    {
        get { return linkedUI; }
        set
        {
            linkedUI = value;
            value.SetData(data);
        }
    }

    public void Start()
    {
        player = MainSceneManager.Instance.GetPlayer();
        player.missileInMaking.Add(this);

        player.TurnFinishAction += turnFinishAct;
        Debug.Log(player.TurnFinishAction);
    }

    public MissileData GetData()
    {
        return data;
    }

    /// <summary>
    /// 생산 대기중인 미사일이 턴이 끝나면 해야할 행동
    /// </summary>
    public void turnFinishAct()
    {
        this.data.turnForMissileReady--;
        linkedUI.SetData(data);
        if (data.turnForMissileReady == 0)
        {
            player.missileInMaking.Remove(this);
            player.missileReadyToShoot.Add(this);

            linkedUI.gameObject.SetActive(false);
            linkedUI = null;

            player.TurnFinishAction -= turnFinishAct;
        }
    }

    public void Fire(TileScript targetTile) // TODO : 미사일 오브젝트 풀에서 하나 뽑아와서 쏘도록 만드셈.
    {
        player.missileReadyToShoot.Remove(this);
    }
    
    public MissileScript(MissileData data)
    {
        this.data = data;
        Start();
    }
}
