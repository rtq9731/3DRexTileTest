using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMissile : MonoBehaviour
{
    private PlayerScript player = null;

    [SerializeField] PanelMissileQueue panelMissileQueue = null;
    [SerializeField] Button btnMakeMissile = null;



    private void Start()
    {
        btnMakeMissile.onClick.AddListener(OnClickMakeMissile);
    }

    private void OnEnable()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
    }


    public void OnClickMakeMissile()
    {
        player.MissileInMaking.Add(new MissileData(10, MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead));
        panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
    }
}
