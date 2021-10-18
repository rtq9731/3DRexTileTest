using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelMissileFireSelect : MonoBehaviour
{
    [SerializeField] LayerMask whereIsTile;
    [SerializeField] Text text; 
    [SerializeField] GameObject vcamFireMissile = null;
    [SerializeField] GameObject vcamMain = null; // 처음에 비활성화, 꺼질때 활성화

    RaycastHit hit;

    InputState state = InputState.None;

    private TileScript startedTile = null;
    private TileScript selectedTile = null;

    private List<MissileData> fireMissiles = new List<MissileData>(); // 발사 대기중인 미사일 ( 선택 X )

    private List<TileScript> tilesInRange = new List<TileScript>();

    bool isOnFireMissile = false; // 추가입력 방지용 플래그

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
    }

    private void Update()
    {
        if(!isOnFireMissile)
        {
            return;
        }

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
                        startedTile = tile;
                        tile.transform.DOMoveY(tile.transform.position.y + 0.3f, 0.5f);
                        tile.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        FireReady(fireMissiles[0]);
                        return;
                    case InputState.SelectTargetTile:
                        if(tilesInRange.Contains(tile)) // 만약 선택한 곳이 사거리 내라면
                        {
                            SetTargetAndFire(fireMissiles[0], tile);
                        }
                        return;
                    case InputState.Finish:
                        return;
                    default:
                        break;
                }
            }
        }


        switch (state)
        {
            case InputState.None:
                break;
            case InputState.SelectStartTile:
                text.text = "미사일의 출발 지점을 정해주세요.\n만약 근처에 적 타일이 없다면\n자동으로 취소됩니다.";
                break;
            case InputState.SelectTargetTile:
                text.text = "타격 지점을 지정해주세요!\n초록색 : 타격 가능 지점\n빨간색 : 타격 불가능 지점\n노란색 : 출발 지점";
                break;
            case InputState.Finish:
                break;
            default:
                break;
        }
    }

    public void InitPanelMissileFire(TileScript tile, List<MissileData> fireMissiles)
    {
        state = InputState.SelectStartTile;

        tilesInRange = new List<TileScript>();

        this.fireMissiles = fireMissiles;

        fireMissiles.Sort((x, y) => -x.MissileRange.CompareTo(y.MissileRange));

        startedTile = tile;

        vcamMain.SetActive(false);
        vcamFireMissile.SetActive(true);

        foreach (var item in fireMissiles)
        {
            MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, item.MissileRange);
        }
    }

    // 미사일 쏠 준비를 해줌
    private void FireReady(MissileData missile)
    {
        TileMapData.Instance.ResetColorAllTile();

        if (fireMissiles.Contains(missile))
        {
            fireMissiles.Remove(missile);
        }

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

    }

    enum InputState
    {
        None,
        SelectStartTile,
        SelectTargetTile,
        Finish
    }
}
