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
    [SerializeField] CinemachineVirtualCamera vcamLookMissile = null;
    [SerializeField] GameObject vcamSelectFire = null;

    [SerializeField] PlayerInput mainInput = null;
    [SerializeField] GameObject vcamMain = null; // 둘 전부 처음에 비활성화, 꺼질때 활성화

    RaycastHit hit;

    InputState state = InputState.None;

    private TileScript startedTile = null;

    private List<MissileData> fireMissiles = new List<MissileData>(); // 발사 대기중인 미사일 ( 선택 X )

    private List<TileScript> tilesInRange = new List<TileScript>();

    bool bStopGetInput = false; // 추가입력 방지용 플래그

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

        if(vcamSelectFire != null)
        {
            vcamSelectFire.gameObject.SetActive(false);
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIStackManager.RemoveUIOnTop();
        }

        switch (state)
        {
            case InputState.None:
                break;
            case InputState.SelectStartTile:
                text.text = "미사일의 출발 지점을 정해주세요.\n만약 근처에 적 타일이 없다면\n자동으로 취소됩니다.";
                break;
            case InputState.SelectTargetTile:
                text.text = "타격 지점을 지정해주세요!\n초록색 : 타격 가능 지점\n빨간색 : 타격 불가능 지점";
                break;
            case InputState.Finish:
                UIStackManager.RemoveUIOnTop();
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
                                UIStackManager.RemoveUIOnTop();
                                return;
                            }

                            startedTile = tile;
                            tile.transform.DOMoveY(tile.transform.position.y + 0.3f, 0.5f);
                            tile.GetComponent<MeshRenderer>().material.color = Color.yellow;

                            vcamSelectFire.transform.position = vcamMain.transform.position;

                            FireReady(fireMissiles[0]);
                            break;
                        case InputState.SelectTargetTile:
                            if (isTileCanFire(tile)) // 만약 선택한 곳이 사거리 내라면
                            {
                                SetTargetAndFire(fireMissiles[0], tile);
                            }
                            break;
                        case InputState.Finish:
                            UIStackManager.RemoveUIOnTop();
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
        return MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, missile.MissileRange).Find(tile =>
        {
            return tile.Owner != MainSceneManager.Instance.GetPlayer() // 주인이 플레이어가 아니어야함
            && tile.Owner != null // 주인 없는 땅은 못때리게
            && !tile.transform.GetComponentInChildren<CloudObject>()// 시야밖의 타일 타격 불가
            && tile.Data.type != TileType.Ocean
            && tile.Data.type != TileType.Lake; // 물타일 타격 불가
        }) != null && tile.Owner == MainSceneManager.Instance.GetPlayer();
    }

    public void InitPanelMissileFire(List<MissileData> fireMissiles)
    {
        state = InputState.SelectStartTile;

        tilesInRange = new List<TileScript>();

        this.fireMissiles = fireMissiles;

        fireMissiles.Sort((x, y) => -x.MissileRange.CompareTo(y.MissileRange));

        mainInput.enabled = false;

        vcamSelectFire.transform.position = vcamMain.transform.position;
        vcamMain.SetActive(false);
        gameObject.SetActive(true);
        vcamSelectFire.gameObject.SetActive(true); 
    }

    // 미사일 쏠 준비를 해줌
    private void FireReady(MissileData missile)
    {
        TileMapData.Instance.ResetColorAllTile();
        
        startedTile.GetComponent<MeshRenderer>().material.color = Color.blue;

        SetCanFireTile(startedTile, missile);
        SetCantFireTile();

        state = InputState.SelectTargetTile;

    }

    /// <summary>
    /// 사거리안에 있는 타일을 업데이트 및 색칠 해줍니다.
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
    /// 사격 불가능한 타일을 골라줍니다.
    /// SetCanFireTile() 다음에 실행되는 것이 좋습니다.
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

    public bool isTileCanFire(TileScript tile)
    {
        return
            tilesInRange.Contains(tile)
            && tile.Owner != MainSceneManager.Instance.GetPlayer() // 주인이 플레이어가 아니어야함
            && tile.Owner != null // 주인 없는 땅은 못때리게
            && !tile.transform.GetComponentInChildren<CloudObject>()// 시야밖의 타일 타격 불가
            && tile.Data.type != TileType.Ocean
            && tile.Data.type != TileType.Lake; // 물타일 타격 불가
    }

    private void SetTargetAndFire(MissileData missile, TileScript target)
    {

        if (fireMissiles.Contains(missile))
        {
            fireMissiles.Remove(missile);
        }


        fireMissiles.Remove(missile);
        MainSceneManager.Instance.missileManager.fireMissileFromStartToTarget(startedTile, missile, target, out GameObject missileObj);
        vcamLookMissile.gameObject.SetActive(true);
        vcamLookMissile.m_LookAt = missileObj.transform;

        StartCoroutine(LookMissileUntailImpact(missileObj));
    }

    IEnumerator LookMissileUntailImpact(GameObject Missile)
    {

        vcamSelectFire.GetComponent<CameraMove>().enabled = false;
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

        vcamLookMissile.m_LookAt = null;
        vcamLookMissile.gameObject.SetActive(false);
        vcamLookMissile.transform.rotation = vcamSelectFire.transform.rotation;
        vcamLookMissile.transform.position = vcamSelectFire.transform.position;

        vcamSelectFire.GetComponent<CameraMove>().enabled = true;
    }

    enum InputState
    {
        None,
        SelectStartTile,
        SelectTargetTile,
        Finish
    }
}
