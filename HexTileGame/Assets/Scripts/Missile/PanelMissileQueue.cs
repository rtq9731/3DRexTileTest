using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMissileQueue : MonoBehaviour
{
    [SerializeField] GameObject missileQueuePanelPrefab;
    [SerializeField] Transform infoPanelParent;

    List<GameObject> queuePanelPool = new List<GameObject>();

    private void OnDisable()
    {
        queuePanelPool.ForEach(x => x.gameObject.SetActive(false));
    }

    public void RefreshMissileQueue(List<MissileData> missiles)
    {
        queuePanelPool.ForEach(x => x.SetActive(false));
        foreach (var item in missiles)
        {
            Debug.Log(item);
            GetQueuePanel(out MissileMakeInfo info).SetActive(true);
            info.SetData(item);
        }
    }

    private GameObject GetQueuePanel(out MissileMakeInfo info)
    {
        GameObject result = null;
        info = null;
        if(queuePanelPool.Count > 1)
        {
            result = queuePanelPool.Find(x => !x.activeSelf);
            if(result != null)
            {
                info = result.GetComponent<MissileMakeInfo>();
            }
            else
            {
                result = Instantiate(missileQueuePanelPrefab, infoPanelParent);
                queuePanelPool.Add(result);
                info = result.GetComponent<MissileMakeInfo>();
            }
        }
        else
        {
            result = Instantiate(missileQueuePanelPrefab, infoPanelParent);
            queuePanelPool.Add(result);
            info = result.GetComponent<MissileMakeInfo>();
        }

        return result;
    }

}
