using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMissileFireSelect : MonoBehaviour
{
    [SerializeField] LayerMask whereIsTile;
    [SerializeField] GameObject vcamFireMissile = null;
    [SerializeField] GameObject vcamMain = null; // 처음에 비활성화, 꺼질때 활성화

    RaycastHit hit;

    public List<MissileData> missilesToFire = new List<MissileData>();

    private TileScript startedTile = null;
    private TileScript selectedTile = null;

    private List<MissileData> fireMissiles = new List<MissileData>();

    bool isGetInput = false;

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
        if(!isGetInput)
        {
            return;
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Camera.main.farClipPlane, whereIsTile))
        {
            TileScript tile = hit.transform.GetComponent<TileScript>();
            if (tile != null)
            {
                
            }
        }
    }

    public void InitPanelMissileFire(TileScript tile, List<MissileData> fireMissiles)
    {
        this.fireMissiles = fireMissiles;

        startedTile = tile;

        vcamMain.SetActive(false);
        vcamFireMissile.SetActive(true);

        foreach (var item in fireMissiles)
        {
            MainSceneManager.Instance.tileChecker.FindTilesInRange(tile, item.MissileRange);
        }
    }

    private void FireMissile(MissileData missile)
    {
        
    }
}
