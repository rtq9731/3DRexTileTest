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

    public MissileData missileBluePrint = null;

    private void Start()
    {
        selector.panelMissileMaker = this;
        btnMakeMissile.onClick.AddListener(OnClickMakeMissile);
    }

    

    private void OnEnable()
    {
        missileBluePrint = new MissileData(MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead);

        if (player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);

        // btnSelectMaterial.onClick.AddListener(() => OnClickSelectpart(partType.Material)); 재질 개발 예정
        btnSelectMaterial.interactable = false;
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
        player.MissileInMaking.Add(missileBluePrint);
        panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
    }
}
