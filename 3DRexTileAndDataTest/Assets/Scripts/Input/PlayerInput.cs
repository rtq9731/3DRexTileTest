using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTile;
    [SerializeField] SimpleTileInfoPanel panel;

    RaycastHit hit;

    bool isSimplePanelOn = false;
    bool isInputSimplePanel = true;

    TileData lastTileData;
    TileData nowData;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

        }

        if(isInputSimplePanel) // 다른 UI가 아무것도 올라가있지 않을 때.
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
