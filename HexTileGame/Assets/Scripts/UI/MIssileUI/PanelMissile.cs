using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMissile : MonoBehaviour
{
    [SerializeField] Button btnMakeMissile;

    [SerializeField] GameObject panelMissileQueueElementPrefab;
    [SerializeField] GameObject PanelMissileQueue;

    Queue<PanelMissileQueueElement> panelMissileQueuePool = new Queue<PanelMissileQueueElement>();

    MissileData missileBlueprint = new MissileData(10, MissileTypes.MissileEngineType.Tier4Engine, MissileTypes.MissileWarheadType.CommonTypeWarhead);

    PlayerScript player = null;

    private void Awake()
    {
        btnMakeMissile.onClick.AddListener(MakeNewMissile);
    }

    private void OnEnable()
    {
        UIStackManager.RemoveUIOnTopWithNoTime();

        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        foreach (var item in player.missileInMaking)
        {
            if (item.LinkedUI == null)
            {
                item.LinkedUI = GetPanelMissileQueueElement();
            }
        }
    }

    private void MakeNewMissile()
    {
        MissileScript newMissile = new MissileScript(missileBlueprint);
        player.missileInMaking.Add(newMissile);
        newMissile.LinkedUI = GetPanelMissileQueueElement();
    }

    private PanelMissileQueueElement GetPanelMissileQueueElement()
    {
        PanelMissileQueueElement result = null;
        if(panelMissileQueuePool.Count > 1)
        {
            if(panelMissileQueuePool.Peek().gameObject.activeSelf == true)
            {
                GameObject tmp = Instantiate(panelMissileQueueElementPrefab, PanelMissileQueue.transform);
                result = tmp.GetComponent<PanelMissileQueueElement>();
                panelMissileQueuePool.Enqueue(result);
            }
            else
            {
                result = panelMissileQueuePool.Dequeue();
                panelMissileQueuePool.Enqueue(result); // 맨뒤로 돌려보냄
            }
        }
        else
        {
            GameObject tmp = Instantiate(panelMissileQueueElementPrefab, PanelMissileQueue.transform);
            result = tmp.GetComponent<PanelMissileQueueElement>();
            panelMissileQueuePool.Enqueue(result);
        }

        result.gameObject.SetActive(true);
        return result;
    }

}
