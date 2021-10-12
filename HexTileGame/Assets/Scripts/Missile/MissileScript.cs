using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript
{
    MissileData data = null;
    PlayerScript player = null;

    public void Start()
    {
        player = MainSceneManager.Instance.GetPlayer();

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
        if (data.turnForMissileReady == 0)
        {
            player.TurnFinishAction -= turnFinishAct;
        }
    }

    public void Fire(TileScript targetTile) // TODO : �̻��� ������Ʈ Ǯ���� �ϳ� �̾ƿͼ� ��� �����.
    {

    }
    
    public MissileScript(MissileData data)
    {
        this.data = data;
        Start();
    }
}
