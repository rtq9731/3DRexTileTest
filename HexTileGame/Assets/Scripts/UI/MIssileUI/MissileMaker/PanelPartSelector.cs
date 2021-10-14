using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPartSelector : MonoBehaviour
{
    [SerializeField] Transform unlockedPartParent = null;
    [SerializeField] PanelPartinfo infoPanelPrefab = null;

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

    public void InitPanelInNull()
    {
        textPartName.text = "선택된 부품 없음";
        textWeight.text = "";
        textMakeTurn.text = "";
        textPartInfo.text = "";
    }

    public void InitPartSelector(MissileWarheadData warhead)
    {
        textPartATK.transform.parent.gameObject.SetActive(true);

        textPartName.text = warhead.Name;
        textWeight.text = $"{warhead.Weight}";
        textMakeTurn.text = $"{warhead.Makingtime}";
        textPartInfo.text = warhead.Info;
    }

    public void InitPartSelector(MissileEngineData engine)
    {
        textPartATK.transform.parent.gameObject.SetActive(false);

        textPartName.text = engine.Name;
        textMakeTurn.text = $"{engine.Makingtime}";
        textPartInfo.text = engine.Info;
    }

    private GameObject GetNewInfoPanel(out PanelPartinfo info)
    {

    }
}
