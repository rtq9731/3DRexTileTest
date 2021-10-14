using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartSelector : MonoBehaviour
{
    [SerializeField] Transform unlockedPartParent = null;
    [SerializeField] GameObject infoPanelPrefab = null;

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
        textPartName.text = "선택된 부품 없음";
        textWeight.text = "";
        textMakeTurn.text = "";
        textPartInfo.text = "";
    }

    public void InitPartText(MissileWarheadData warhead)
    {
        textPartATK.transform.parent.gameObject.SetActive(true);

        textPartName.text = warhead.Name;
        textWeight.text = $"{warhead.Weight}";
        textMakeTurn.text = $"{warhead.Makingtime}";
        textPartInfo.text = warhead.Info;
    }

    public void InitPartText(MissileEngineData engine)
    {
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
