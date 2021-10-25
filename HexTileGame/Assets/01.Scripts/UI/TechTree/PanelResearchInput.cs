using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResearchInput : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] Image researchIcon = null;
    [SerializeField] Text textResearchName = null;
    [SerializeField] Text textResearchTime = null;
    [SerializeField] Text textWeight = null;
    [SerializeField] Text textDamage = null;
    [SerializeField] Text textResource = null;
    [SerializeField] Text textResearchInfo = null;

    [Header("btns")]
    [SerializeField] Button btnOk;
    [SerializeField] Button btnCancel;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="callBack">사용자의 입력을 돌려줍니다</param>
    public void InitPanelInput(SkillTreeNode data, Action callBack)
    {
        gameObject.SetActive(true);
        switch (data.Type)
        {
            case ResearchType.Warhead:
                MissileWarheadData warheadData = MainSceneManager.Instance.GetWarheadByIdx(data.ResearchThingIdx);
                textResearchName.text = warheadData.Name;
                textWeight.text = $"탄두 무게 : {warheadData.Weight}";
                textDamage.text = $"탄두 공격력 : {warheadData.Atk}";
                textResearchInfo.text = $"{warheadData.Info}";
                break;
            case ResearchType.Engine:
                break;
            case ResearchType.Material:
                break;
            default:
                break;
        }

        textResearchTime.text = $"소요 시간 : {data.TrunForResearch} 턴";
        textResource.text = $"연구에 소모되는 자원 : {data.ResearchResource}";

        btnOk.onClick.RemoveAllListeners();
        btnCancel.onClick.RemoveAllListeners();

        btnOk.onClick.AddListener(() =>
        {
            callBack();
            UIStackManager.RemoveUIOnTop();
        });
        btnCancel.onClick.AddListener(() => UIStackManager.RemoveUIOnTop());
    }
}
