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
        if (isUIEmpty && Camera.main.transform.position == cameraMoveScript.transform.position) // �ٸ� UI�� �ƹ��͵� �ö����� ���� �� / ī�޶� �ִϸ��̼��� ������ ��
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
                    if(pointedTile != null && hit.transform.GetComponentInChildren<CloudObject>() == null) // Ÿ���̰�, �þ߾ȿ� ���� ��
                    {
                        TileInfoScript.TurnOnTileInfoPanel(hit.transform.GetComponent<TileScript>());
                    }
                }
            }
            else // ���콺 �Է��� ���� ��
            {
                if (isTileHit)
                {
                    if (pointedTile != null && hit.transform.GetComponentInChildren<CloudObject>() == null && Time.time >= lastTileHitTime + tileHitCheckInterval) // Ÿ���̰�, �þ� �ȿ� ���� ��
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
