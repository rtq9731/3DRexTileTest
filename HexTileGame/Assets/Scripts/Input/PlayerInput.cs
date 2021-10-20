using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] SimpleTileInfoPanel panel;
    [SerializeField] GameObject GameExitPanel;
    [SerializeField] CameraMove cameraMoveScript;
    [SerializeField] LayerMask whereIsTile;

    RaycastHit hit;

    bool isSimplePanelOn = false;
    bool isUIEmpty;

    TileData lastTileData;
    TileData nowData;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIStackManager.RemoveUIOnTop())
            {
                GameExitPanel.SetActive(true);
            }
        }

        isUIEmpty = UIStackManager.IsUIStackEmpty();
        if (isUIEmpty && Camera.main.transform.position == cameraMoveScript.transform.position) // 다른 UI가 아무것도 올라가있지 않을 때 / 카메라 애니메이션이 끝났을 때
        {
            cameraMoveScript.enabled = isUIEmpty;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whereIsTile))
                {
                    if(hit.transform.GetComponent<TileScript>() != null)
                    {
                        TileInfoScript.TurnOnTileInfoPanel(hit.transform.GetComponent<TileScript>());
                    }
                }
            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whereIsTile))
            {
                if (hit.transform.GetComponent<TileScript>() == null)
                {
                    return;
                }

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
        else
        {
            isSimplePanelOn = false;
            panel.RemoveSimpleTileInfoPanel();

            if (cameraMoveScript.enabled)
            {
                cameraMoveScript.enabled = isUIEmpty;
            }
        }    
    }

    IEnumerator GetNextData()
    {
        yield return new WaitForSeconds(0.5f);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whereIsTile))
        {
            if (!EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
            {
                if (hit.transform.GetComponent<TileScript>() != null)
                {
                    lastTileData = hit.transform.GetComponent<TileScript>().Data;
                    if (lastTileData == nowData)
                    {
                        panel.CallSimpleTileInfoPanel(hit.transform.GetComponent<TileScript>());
                    }
                }
            }
        }
    }

}
