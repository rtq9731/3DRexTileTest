using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTile;
    [SerializeField] SimpleTileInfoPanel panel;

    RaycastHit hit;

    bool isSimplePanelOn = false;

    TileData lastTileData;
    TileData nowData;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whatIsTile))
        {
            nowData = hit.transform.GetComponent<TileScript>().Data;
            if (nowData != lastTileData)
            {
                isSimplePanelOn = false;
                panel.RemoveSimpleTileInfoPanel();
                lastTileData = nowData;
            }
            else
            {
                if (isSimplePanelOn)
                    return;

                isSimplePanelOn = true;
                StartCoroutine(GetNextData());
            }
        }
    }

    IEnumerator GetNextData()
    {
        yield return new WaitForSeconds(0.5f);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whatIsTile))
        {
            lastTileData = hit.transform.GetComponent<TileScript>().Data;
            if (lastTileData == nowData)
            {
                panel.CallSimpleTileInfoPanel(hit.transform.GetComponent<TileScript>());
            }
        }
    }

}
