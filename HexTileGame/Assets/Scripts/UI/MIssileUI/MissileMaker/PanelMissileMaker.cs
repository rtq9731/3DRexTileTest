using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMissileMaker : MonoBehaviour
{
    private PlayerScript player = null;

    [SerializeField] PanelMissileQueue panelMissileQueue = null;
    [SerializeField] Button btnMakeMissile = null;

    [Header("About Parts")]
    [SerializeField] PanelPartSelector selector;
    [SerializeField] Button btnSelectWarhead;
    [SerializeField] Button btnSelectMaterial;
    [SerializeField] Button btnSelectEngine;


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

        btnSelectMaterial.onClick.AddListener(() => OnClickSelectpart(partType.Material));
        btnSelectEngine.onClick.AddListener(() => OnClickSelectpart(partType.Engine));
        btnSelectWarhead.onClick.AddListener(() => OnClickSelectpart(partType.Warhead));
    }

    public void OnClickSelectpart(partType part)
    {
        selector.InitPanelInNull(part);
    }

    public enum partType
    {
        Material,
        Engine,
        Warhead
    };

    public void OnClickMakeMissile()
    {
        player.MissileInMaking.Add(new MissileData(10, MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead));
        panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
    }
}
