using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartSelector : MonoBehaviour
{
    [SerializeField] Transform unlockedPartParent = null;
    [SerializeField] GameObject infoPanelPrefab = null;
    [SerializeField] Button btnOk = null;

    public PanelMissileMaker panelMissileMaker;

    List<GameObject> partPanelPool = new List<GameObject>();

    [Header("About MissilePart Info")]
    [SerializeField] Image partIcon = null;
    [SerializeField] Text textPartName = null;
    [SerializeField] Text textPartATK = null;
    [SerializeField] Text textMakeTurn = null;
    [SerializeField] Text textWeight = null;
    [SerializeField] Text textPartInfo = null;

    private PlayerScript player;
    private void OnEnable()
    {
        if(player == null)
        {
            MainSceneManager.Instance.GetPlayer();
        }
    }

    public void InitPanelInNull(PanelMissileMaker.partType part)
    {
        Debug.Log(transform.parent.gameObject);
        transform.parent.gameObject.SetActive(true);

        partPanelPool.ForEach(x => x.gameObject.SetActive(false));

        switch (part)
        {
            case PanelMissileMaker.partType.Material:
                break;
            case PanelMissileMaker.partType.Engine:
                foreach (var item in player.UnlockedEngineIdx)
                {
                    GetNewInfoPanel(out PanelPartinfo info);
                    info.InitPanelPartInfo(MissileEngine.GetMissileDataByIdx(item));
                }
                break;
            case PanelMissileMaker.partType.Warhead:
                foreach (var item in player.unlockedWarheadIdx)
                {
                    GetNewInfoPanel(out PanelPartinfo info);
                    info.InitPanelPartInfo(MissileWarhead.GetWarheadByIdx(item));

                }
                break;
            default:
                break;
        }

        textPartName.text = "선택된 부품 없음";
        textWeight.text = "";
        textMakeTurn.text = "";
        textPartInfo.text = "";
    }

    public void InitPartText(MissileWarheadData warhead)
    {
        btnOk.onClick.AddListener(() =>
        {
            panelMissileMaker.missileBluePrint.WarheadType = warhead.TYPE;
            btnOk.onClick.RemoveAllListeners();
        });

        textPartATK.transform.parent.gameObject.SetActive(true);

        textPartName.text = warhead.Name;
        textWeight.text = $"{warhead.Weight}";
        textMakeTurn.text = $"{warhead.Makingtime}";
        textPartInfo.text = warhead.Info;
    }

    public void InitPartText(MissileEngineData engine)
    {
        btnOk.onClick.AddListener(() =>
        {
            panelMissileMaker.missileBluePrint.EngineTier = engine.TYPE;
            btnOk.onClick.RemoveAllListeners();
        });

        textPartATK.transform.parent.gameObject.SetActive(false);

        textPartName.text = engine.Name;
        textMakeTurn.text = $"{engine.Makingtime}";
        textPartInfo.text = engine.Info;
    }

    private GameObject GetNewInfoPanel(out PanelPartinfo element)
    {
        GameObject result = null;
        element = null;
        if (partPanelPool.Count > 1)
        {
            result = partPanelPool.Find(x => !x.activeSelf);
            if (result != null)
            {
                element = result.GetComponent<PanelPartinfo>();
            }
            else
            {
                result = Instantiate(infoPanelPrefab, unlockedPartParent);
                partPanelPool.Add(result);
                element = result.GetComponent<PanelPartinfo>();
            }
        }
        else
        {
            result = Instantiate(infoPanelPrefab, unlockedPartParent);
            partPanelPool.Add(result);
            element = result.GetComponent<PanelPartinfo>();
        }

        return result;
    }
}
