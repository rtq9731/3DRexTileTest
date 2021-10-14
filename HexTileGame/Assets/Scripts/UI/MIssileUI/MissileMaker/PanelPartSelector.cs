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

    [SerializeField] private PlayerScript player = null;

    public void InitPanelInNull(PanelMissileMaker.partType part)
    {
        transform.parent.gameObject.SetActive(true);

        partPanelPool.ForEach(x => x.gameObject.SetActive(false));

        switch (part)
        {
            case PanelMissileMaker.partType.Material:
                break;
            case PanelMissileMaker.partType.Engine:
                foreach (var item in player.UnlockedEngineIdx)
                {
                    GetNewInfoPanel(out PanelPartinfo element);
                    Debug.Log(element);
                    element.partSelector = this;
                    element.InitPanelPartInfo(MainSceneManager.Instance.GetEngineDataByIdx(item), unlockedPartParent.GetComponent<ToggleGroup>());
                }
                break;
            case PanelMissileMaker.partType.Warhead:
                foreach (var item in player.unlockedWarheadIdx)
                {
                    GetNewInfoPanel(out PanelPartinfo element);
                    Debug.Log(element);
                    element.partSelector = this;
                    element.InitPanelPartInfo(MainSceneManager.Instance.GetWarheadByIdx(item), unlockedPartParent.GetComponent<ToggleGroup>());

                }
                break;
            default:
                break;
        }

        textPartName.text = "선택된 부품 없음";
        textPartATK.text = "";
        textWeight.text = "";
        textMakeTurn.text = "";
        textPartInfo.text = "";
    }

    public void InitPartText(MissileWarheadData warhead)
    {
        btnOk.onClick.AddListener(() =>
        {
            panelMissileMaker.missileBluePrint.WarheadType = warhead.TYPE;
            transform.parent.gameObject.SetActive(false);
            btnOk.onClick.RemoveAllListeners();
        });

        textPartATK.transform.parent.gameObject.SetActive(true);

        textPartName.text = warhead.Name;
        textPartATK.text = $"탄두의 공격력 : {warhead.Atk}";
        textWeight.text = $"탄두의 무게 : {warhead.Weight}";
        textMakeTurn.text = $"제작에 걸리는 시간 : {warhead.Makingtime} 턴";
        textPartInfo.text = warhead.Info;
    }

    public void InitPartText(MissileEngineData engine)
    {
        btnOk.onClick.AddListener(() =>
        {
            panelMissileMaker.missileBluePrint.EngineTier = engine.TYPE;
            transform.parent.gameObject.SetActive(false);
            btnOk.onClick.RemoveAllListeners();
        });

        textPartATK.transform.parent.gameObject.SetActive(false);

        textPartName.text = engine.Name;
        textMakeTurn.text = $"제작에 걸리는 시간 : {engine.Makingtime} 턴";
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

        result.SetActive(true);
        return result;
    }
}
