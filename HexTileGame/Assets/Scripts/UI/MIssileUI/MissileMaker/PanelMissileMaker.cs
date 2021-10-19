using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMissileMaker : MonoBehaviour
{
    private PlayerScript player = null;

    [SerializeField] PanelMissileQueue panelMissileQueue = null;
    [SerializeField] Button btnMakeMissile = null;
    [SerializeField] MakeMissileInfo makeMissileInfo = null;

    [Header("About Parts")]
    [SerializeField] PanelPartSelector selector;
    [SerializeField] Button btnSelectWarhead;
    [SerializeField] Button btnSelectMaterial;
    [SerializeField] Button btnSelectEngine;

    [Header("About MissileInfo")]
    [SerializeField] Text textATK = null;
    [SerializeField] Text textRange = null;
    [SerializeField] Text textTurn = null;
    [SerializeField] Text textMissilesInMake = null;

    private MissileData missileBluePrint = null;

    public void SetMissileBluePrintPart(MissileTypes.MissileEngineType engine)
    {
        missileBluePrint.EngineTier = engine;
        RefreshMissileInfoTexts();
    }

    public void SetMissileBluePrintPart(MissileTypes.MissileWarheadType warhead)
    {
        missileBluePrint.WarheadType = warhead;
        RefreshMissileInfoTexts();
    }

    private void RefreshMissileInfoTexts()
    {
        makeMissileInfo.InitPanelMissileInfo(missileBluePrint);
        textMissilesInMake.text = $"{ player.MissileInMaking.Count + player.MissileReadyToShoot.Count } / { player.OwningTiles.Count }";
    }

    private void Start()
    {
        selector.panelMissileMaker = this;
        btnMakeMissile.onClick.AddListener(OnClickMakeMissile);

        // btnSelectMaterial.onClick.AddListener(() => OnClickSelectpart(partType.Material)); 재질 개발 예정
        btnSelectMaterial.interactable = false;
        btnSelectEngine.onClick.AddListener(() => OnClickSelectpart(partType.Engine));
        btnSelectWarhead.onClick.AddListener(() => OnClickSelectpart(partType.Warhead));
    }

    

    private void OnEnable()
    {
        missileBluePrint = new MissileData(MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead);

        if (player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        textMissilesInMake.text = $"{ player.MissileInMaking.Count + player.MissileReadyToShoot.Count } / { player.OwningTiles.Count }";
        panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
        RefreshMissileInfoTexts();

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
        if(missileBluePrint.MissileRange > 0 && player.MissileInMaking.Count + player.MissileReadyToShoot.Count < player.OwningTiles.Count)
        {
            player.MissileInMaking.Add(new MissileData(missileBluePrint.EngineTier, missileBluePrint.WarheadType));
            panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
            textMissilesInMake.text = $"{ player.MissileInMaking.Count + player.MissileReadyToShoot.Count } / { player.OwningTiles.Count }";
        }
    }
}
