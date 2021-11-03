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

    WaitForSeconds ws;

    TileData lastTileData;
    float lastTileHitTime = 0f;
    TileData nowData;

    float tileHitCheckInterval = 0f;

    private void Start()
    {
        ws = new WaitForSeconds(0.3f);
    }

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

            bool isTileHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whereIsTile);

            TileScript pointedTile = null;
            if (hit.transform != null)
            {
                pointedTile = hit.transform.GetComponent<TileScript>();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                if (isTileHit)
                {
                    if(pointedTile != null && hit.transform.GetComponentInChildren<CloudObject>() == null) // 타일이고, 시야안에 있을 때
                    {
                        TileInfoScript.TurnOnTileInfoPanel(hit.transform.GetComponent<TileScript>());
                    }
                }
            }
            else // 마우스 입력이 없을 때
            {
                if (isTileHit)
                {
                    if (pointedTile != null && hit.transform.GetComponentInChildren<CloudObject>() == null && Time.time >= lastTileHitTime + tileHitCheckInterval) // 타일이고, 시야 안에 있을 때
                    {
                        lastTileHitTime = Time.time;
                        panel.CallSimpleTileInfoPanel(pointedTile);
                    }
                    else
                    {
                        panel.RemoveSimpleTileInfoPanel();
                    }
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

}
