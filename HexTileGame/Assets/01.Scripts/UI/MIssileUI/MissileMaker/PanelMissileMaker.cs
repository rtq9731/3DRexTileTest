using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMissileMaker : MonoBehaviour
{
    private PlayerScript player = null;

    [SerializeField] PanelMissileQueue panelMissileQueue = null;
    [SerializeField] Button btnMakeMissile = null;
    [SerializeField] Button btnResearch = null;
    [SerializeField] MakeMissileInfo makeMissileInfo = null;

    [Header("About Parts")]
    [SerializeField] PanelPartSelector selector;
    [SerializeField] Button btnSelectWarhead;
    [SerializeField] Button btnSelectBody;
    [SerializeField] Button btnSelectEngine;

    [Header("About MissileInfo")]
    [SerializeField] Text textMissilesInMake = null;

    [Header("Other")]
    [SerializeField] GameObject missileTechTreePanel = null;

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
    public void SetMissileBluePrintPart(MissileTypes.MissileBody body)
    {
        missileBluePrint.BodyType = body;
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
        btnResearch.onClick.AddListener(OnClickResearch);

        btnSelectBody.onClick.AddListener(() => OnClickSelectpart(partType.Body));
        btnSelectEngine.onClick.AddListener(() => OnClickSelectpart(partType.Engine));
        btnSelectWarhead.onClick.AddListener(() => OnClickSelectpart(partType.Warhead));
    }

    

    private void OnEnable()
    {
        missileBluePrint = new MissileData(MissileTypes.MissileEngineType.commonEngine, MissileTypes.MissileWarheadType.CommonTypeWarhead, MissileTypes.MissileBody.Orign);

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
        Body,
        Engine,
        Warhead
    };

    public void OnClickResearch()
    {
        UIStackManager.RemoveUIOnTop();
        missileTechTreePanel.gameObject.SetActive(true);
    }

    public void OnClickMakeMissile()
    {
        if(missileBluePrint.MissileRange > 0 && player.MissileInMaking.Count + player.MissileReadyToShoot.Count < player.OwningTiles.Count && missileBluePrint.CanMakeIt())
        {
            player.MissileInMaking.Add(new MissileData(missileBluePrint.EngineTier, missileBluePrint.WarheadType, missileBluePrint.BodyType));
            panelMissileQueue.RefreshMissileQueue(player.MissileInMaking);
            textMissilesInMake.text = $"{ player.MissileInMaking.Count + player.MissileReadyToShoot.Count } / { player.OwningTiles.Count }";
        }
    }
}
