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

    private List<MissileData> fireMissiles = new List<MissileData>(); // �߻� ������� �̻��� ( ���� X )

    private List<TileScript> tilesInRange = new List<TileScript>();

    bool bStopGetInput = false; // �߰��Է� ������ �÷���

    private void OnDisable()
    {
        if(fireMissiles.Count >= 1)
        {
            foreach (var item in fireMissiles)
            {
                if (!MainSceneManager.Instance.GetPlayer().MissileReadyToShoot.Contains(item))
                MainSceneManager.Instance.GetPlayer().MissileReadyToShoot.Add(item);
            }
        }

        if(TileMapData.Instance != null)
        {
            TileMapData.Instance.ResetColorAllTile();
        }

        if (vcamMain != null)
        {
            vcamMain.SetActive(true);
        }

        if(mainInput != null)
        {
            mainInput.enabled = true;
        }

        if(startedTile != null)
        {
            startedTile.transform.position = startedTile.Data.Position;
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
                text.text = "Ÿ�� ������ �������ּ���!\n�ʷϻ� : Ÿ�� ���� ����\n������ : Ÿ�� �Ұ��� ����\n�Ķ��� : ��� ����";
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
                            if(!isCanStartTile(tile, fireMissiles[0]))
                            {
                                gameObject.SetActive(false);
                                return;
                            }

                            startedTile = tile;
                            tile.transform.DOMoveY(tile.transform.position.y + 0.3f, 0.5f);
                            tile.GetComponent<MeshRenderer>().material.color = Color.yellow;

                            vcamFireMissile.transform.position = vcamMain.transform.position;

                            FireReady(fireMissiles[0]);
                            break;
                        case InputState.SelectTargetTile:
                            if (isTileCanFire(tile)) // ���� ������ ���� ��Ÿ� �����
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

    public bool isCanStartTile(TileScript tile, MissileData missile)
    {
        return MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, missile.MissileRange).Find( x => 
        {
            return x.Owner != MainSceneManager.Instance.GetPlayer() // ������ �÷��̾ �ƴϾ����
            && tile.Owner != null // ���� ���� ���� ��������
            && !x.transform.GetComponentInChildren<CloudObject>()// �þ߹��� Ÿ�� Ÿ�� �Ұ�
            && x.Data.type != TileType.Ocean
            && x.Data.type != TileType.Lake; // ��Ÿ�� Ÿ�� �Ұ�
        }) != null; // Ÿ�� ������ Ÿ���� �ִ��� �˻�
    }

    public bool isTileCanFire(TileScript tile)
    {
        return 
            tilesInRange.Contains(tile)
            && tile.Owner != MainSceneManager.Instance.GetPlayer() // ������ �÷��̾ �ƴϾ����
            && tile.Owner != null // ���� ���� ���� ��������
            && !tile.transform.GetComponentInChildren<CloudObject>()// �þ߹��� Ÿ�� Ÿ�� �Ұ�
            && tile.Data.type != TileType.Ocean  
            && tile.Data.type != TileType.Lake; // ��Ÿ�� Ÿ�� �Ұ�
    }

    public void InitPanelMissileFire(List<MissileData> fireMissiles)
    {
        state = InputState.SelectStartTile;

        tilesInRange = new List<TileScript>();

        this.fireMissiles = fireMissiles;

        fireMissiles.Sort((x, y) => -x.MissileRange.CompareTo(y.MissileRange));

        mainInput.enabled = false;

        vcamFireMissile.transform.position = vcamMain.transform.position;
        vcamMain.SetActive(false);
        gameObject.SetActive(true);
        vcamFireMissile.gameObject.SetActive(true); 
    }

    // �̻��� �� �غ� ����
    private void FireReady(MissileData missile)
    {
        TileMapData.Instance.ResetColorAllTile();
        
        startedTile.GetComponent<MeshRenderer>().material.color = Color.blue;

        SetCanFireTile(startedTile, missile);
        SetCantFireTile();

        state = InputState.SelectTargetTile;

    }

    /// <summary>
    /// ��Ÿ��ȿ� �ִ� Ÿ���� ������Ʈ �� ��ĥ ���ݴϴ�.
    /// </summary>
    /// <param name="startedTile"></param>
    /// <param name="missile"></param>
    private void SetCanFireTile(TileScript startedTile, MissileData missile)
    {
        tilesInRange = MainSceneManager.Instance.tileChecker.FindTilesInRange(startedTile, missile.MissileRange);
        tilesInRange.ForEach(x =>
        {
            x.GetComponent<MeshRenderer>().material.color = Color.green;
        });
    }

    /// <summary>
    /// ��� �Ұ����� Ÿ���� ����ݴϴ�.
    /// SetCanFireTile() ������ ����Ǵ� ���� �����ϴ�.
    /// </summary>
    private void SetCantFireTile()
    {
        var tilesCantFire = from item in TileMapData.Instance.GetAllTiles()
                            where !isTileCanFire(item)
                            select item;

        foreach (var item in tilesCantFire)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.red;
        }
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

        vcamFireMissile.GetComponent<CameraMove>().enabled = false;
        bStopGetInput = true;
        while(Missile.activeSelf)
        {
            yield return null;
        }

        if (fireMissiles.Count >= 1)
        {
            state = InputState.SelectTargetTile;

            TileMapData.Instance.ResetColorAllTile();
            SetCanFireTile(startedTile, fireMissiles[0]);
            SetCantFireTile();
        }
        else
        {
            state = InputState.Finish;
        }
        bStopGetInput = false;
        vcamFireMissile.GetComponent<CameraMove>().enabled = true;
    }

    enum InputState
    {
        None,
        SelectStartTile,
        SelectTargetTile,
        Finish
    }
}
