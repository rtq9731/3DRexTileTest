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
    /// ���� ������� �̻����� ���� ������ �ؾ��� �ൿ
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

    public void Fire(TileScript targetTile) // TODO : �̻��� ������Ʈ Ǯ���� �ϳ� �̾ƿͼ� ��� �����.
    {
        player.missileReadyToShoot.Remove(this);
    }
    
    public MissileScript(MissileData data)
    {
        this.data = data;
        Start();
    }
}
