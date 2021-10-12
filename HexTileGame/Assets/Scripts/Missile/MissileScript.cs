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

        Debug.Log(player.TurnFinishAction);
    }

    public MissileData GetData()
    {
        return data;
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
