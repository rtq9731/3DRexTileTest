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
    [SerializeField] Text textResearchInfo = null;

    [Header("btns")]
    [SerializeField] Button btnOk;
    [SerializeField] Button btnCancel;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="callBack">사용자의 입력을 돌려줍니다</param>
    public void InitPanelInput(SkillTreeNode data, Action callBack, Sprite icon)
    {
        researchIcon.sprite = icon;
        gameObject.SetActive(true);
        RefreshTexts();

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
                if (data.ResearchInfo.Contains("단계")) // 만약 진짜 엔진 연구면
                {
                    Debug.Log(data.ResearchThingIdx);
                    MissileEngineData engineData = MainSceneManager.Instance.GetEngineDataByIdx(data.ResearchThingIdx);
                    textResearchName.text = engineData.Name;
                    textWeight.text = $"감당 가능한 무게 : {engineData.Weight}";
                }
                else if (data.ResearchInfo.Contains("연료")) // 엔진 선행 연구중 연료 연구면
                {
                    textResearchName.text = "효율적인 연료";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "미사일이 더 긴 거리에 있는 적을 타격 할 수 있도록,\n현재 연료로 사용되고 있는 화합물의 배합을 이리저리 바꿔봅니다.\n바꿔보는 과정에서 더욱 효율적인 연료가 만들어지면,\n우리 연구원들이 차세대 미사일 엔진에 적용해 줄 것입니다.";
                }
                else if(data.ResearchInfo.Contains("엔진")) // 엔진 선행 연구중 효율 연구면
                {
                    textResearchName.text = "효율적인 엔진";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "미사일이 더 긴 거리에 있는 적을 타격 할 수 있도록,\n현재 엔진에 사용되고 있는 기술들을 차세대 기술로 바꾸어봅니다.\n프로토타입을 제작하는 과정에서 더욱 효율적인 엔진이 만들어지면,\n우리 연구원들이 차세대 미사일 엔진에 적용해 줄 것입니다.";
                }

                textDamage.gameObject.SetActive(false);
                break;
            case ResearchType.Body:
                if (data.ResearchInfo.Contains("날개")) // 만약 진짜 날개 연구면
                {
                    Debug.Log(data.ResearchThingIdx);
                    BodyData bodyData = MainSceneManager.Instance.GetMissileBodyByIdx(data.ResearchThingIdx);
                    textResearchName.text = bodyData.Name;
                    textWeight.text = $"추가 사거리 : {bodyData.Morerange}";
                }
                else if (data.ResearchInfo.Contains("재질")) // 엔진 선행 연구중 재질 연구면
                {
                    textResearchName.text = "합성 재질 연구";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "선체가 더 나은 재질로 개발 될 수 있도록,\n현재 선체 재질의 성분들을 이리저리 바꿔봅니다.\n바꿔보는 과정에서 더욱 효율적인 선체가 만들어지면,\n우리 연구원들이 차세대 미사일 선체에 적용해 줄 것입니다.";
                }
                else if (data.ResearchInfo.Contains("무게")) // 엔진 선행 연구중 무게 연구면
                {
                    textResearchName.text = "무게 경량화 연구";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "미사일이 더 긴 거리에 있는 적을 타격 할 수 있도록,\n현재 미사일의 선체에 사용되는 금속을 교체해봅니다.\n프로토타입을 제작하는 과정에서 더욱 가벼운 선체가 만들어지면,\n우리 연구원들이 차세대 미사일 선체에 적용해 줄 것입니다.";
                }

                textDamage.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        textResearchTime.text = $"소요 시간 : {data.TrunForResearch} 턴";

        btnOk.onClick.RemoveAllListeners();
        btnCancel.onClick.RemoveAllListeners();

        btnOk.onClick.AddListener(() =>
        {
            callBack();
            UIStackManager.RemoveUIOnTop();
        });
        btnCancel.onClick.AddListener(() => UIStackManager.RemoveUIOnTop());
    }

    private void RefreshTexts()
    {
        textResearchName.gameObject.SetActive(true);
        textResearchTime.gameObject.SetActive(true);
        textWeight.gameObject.SetActive(true);
        textDamage.gameObject.SetActive(true);
        textResearchInfo.gameObject.SetActive(true);
    }
}
