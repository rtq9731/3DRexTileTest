using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class PanelMissileFireSelect : MonoBehaviour
{
    [SerializeField] LayerMask whereIsTile;
    [SerializeField] Text text; 
    [SerializeField] CinemachineVirtualCamera vcamFireMissile = null;

    [SerializeField] PlayerInput mainInput = null;
    [SerializeField] GameObject vcamMain = null; // �� ���� ó���� ��Ȱ��ȭ, ������ Ȱ��ȭ

    RaycastHit hit;

    InputState state = InputState.None;

    private TileScript startedTile = null;
    private TileScript selectedTile = null;

    private List<MissileData> fireMissiles = new List<MissileData>(); // �߻� ������� �̻��� ( ���� X )

    private List<TileScript> tilesInRange = new List<TileScript>();

    bool bStopGetInput = false; // �߰��Է� ������ �÷���

    private void OnDisable()
    {
        if(fireMissiles.Count > 1)
        {
            foreach (var item in fireMissiles)
            {
                if (!MainSceneManager.Instance.GetPlayer().MissileReadyToShoot.Contains(item))
                MainSceneManager.Instance.GetPlayer().MissileReadyToShoot.Add(item);
            }
        }

        if(vcamMain != null)
        {
            vcamMain.SetActive(true);
        }

        if(mainInput != null)
        {
            mainInput.enabled = true;
        }
    }

    private void Update()
    {
        if(bStopGetInput)
        {
            return;
        }

        switch (state)
        {
            case InputState.None:
                break;
            case InputState.SelectStartTile:
                text.text = "�̻����� ��� ������ �����ּ���.\n���� ��ó�� �� Ÿ���� ���ٸ�\n�ڵ����� ��ҵ˴ϴ�.";
                break;
            case InputState.SelectTargetTile:
                text.text = "Ÿ�� ������ �������ּ���!\n�ʷϻ� : Ÿ�� ���� ����\n������ : Ÿ�� �Ұ��� ����\n����� : ��� ����";
                break;
            case InputState.Finish:
                gameObject.SetActive(false);
                break;
            default:
                break;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whereIsTile))
            {
                TileScript tile = hit.transform.GetComponent<TileScript>();
                if (tile != null)
                {
                    switch (state)
                    {
                        case InputState.None:
                            break;
                        case InputState.SelectStartTile:

                            if (tile.Owner == MainSceneManager.Instance.GetPlayer() || tile.Data.type == (TileType.Ocean | TileType.Lake))
                            {
                                gameObject.SetActive(false);
                                return;
                            }

                            if(MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, fireMissiles[0].MissileRange).Find(x => x.Owner != MainSceneManager.Instance.GetPlayer()) == null)
                            {
                                gameObject.SetActive(false);
                                return;
                            }

                            startedTile = tile;
                            tile.transform.DOMoveY(tile.transform.position.y + 0.3f, 0.5f);
                            tile.GetComponent<MeshRenderer>().material.color = Color.yellow;
                            FireReady(fireMissiles[0]);
                            break;
                        case InputState.SelectTargetTile:
                            if (tilesInRange.Contains(tile)) // ���� ������ ���� ��Ÿ� �����
                            {
                                SetTargetAndFire(fireMissiles[0], tile);
                            }
                            break;
                        case InputState.Finish:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void InitPanelMissileFire(List<MissileData> fireMissiles)
    {
        state = InputState.SelectStartTile;

        tilesInRange = new List<TileScript>();

        this.fireMissiles = fireMissiles;

        fireMissiles.Sort((x, y) => -x.MissileRange.CompareTo(y.MissileRange));

        mainInput.enabled = false;
        vcamMain.SetActive(false);
        gameObject.SetActive(true);
        vcamFireMissile.gameObject.SetActive(true);
    }

    // �̻��� �� �غ� ����
    private void FireReady(MissileData missile)
    {
        TileMapData.Instance.ResetColorAllTile();

        tilesInRange = MainSceneManager.Instance.tileChecker.FindTilesInRange(startedTile, missile.MissileRange);

        var tilesCantFire = from item in TileMapData.Instance.GetAllTiles()
                          where !tilesInRange.Contains(item)
                          select item;

        foreach (var item in tilesCantFire)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        
        tilesInRange.ForEach(x => x.GetComponent<MeshRenderer>().material.color = Color.green);
        state = InputState.SelectTargetTile;
    }

    private void SetTargetAndFire(MissileData missile, TileScript target)
    {
        if (fireMissiles.Contains(missile))
        {
            fireMissiles.Remove(missile);
        }

        fireMissiles.Remove(missile);
        MainSceneManager.Instance.missileManager.fireMissileFromStartToTarget(startedTile, missile, target, out GameObject missileObj);
        vcamFireMissile.m_LookAt = missileObj.transform;

        StartCoroutine(LookMissileUntailImpact(missileObj));
    }

    IEnumerator LookMissileUntailImpact(GameObject Missile)
    {
        
        bStopGetInput = true;
        while(Missile.activeSelf)
        {
            yield return null;
        }

        if (fireMissiles.Count >= 1)
        {
            state = InputState.SelectTargetTile;
        }
        else
        {
            state = InputState.Finish;
        }
        bStopGetInput = false;
    }

    enum InputState
    {
        None,
        SelectStartTile,
        SelectTargetTile,
        Finish
    }
}
