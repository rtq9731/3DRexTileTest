using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechNode : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] int idx;
    [SerializeField] Button myBtn = null;

    SkillTreeNode data = null;
    PersonPlayer player = null;

    private void Start()
    {
        myBtn.onClick.AddListener(OnClickCallPanelInput);
    }

    private void OnEnable()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }
        
        if(data == null)
        {
            data = MainSceneManager.Instance.techTreeDatas.GetDataByIdx(idx);
        }

        switch (data.Type) // 타입에 따라 플레이어가 사전 연구해야할걸 가지고 있는지 체크
        {
            case ResearchType.Warhead:
                myBtn.interactable = true;

                foreach (var item in data.RequireResearches)
                {
                    if (!player.UnlockedWarheadIdx.Contains(item))
                    {
                        myBtn.interactable = false;
                        break;
                    }
                }
                break;
            case ResearchType.Engine:
                myBtn.interactable = true;

                foreach (var item in data.RequireResearches)
                {
                    if (!player.ResearchedEngineResearch.Contains(item))
                    {
                        myBtn.interactable = false;
                    }
                }
                break;
            case ResearchType.Body:
                myBtn.interactable = true;

                foreach (var item in data.RequireResearches)
                {
                    if (!player.ResearchedBodyResearch.Contains(item))
                    {
                        myBtn.interactable = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnClickCallPanelInput()
    {
        MainSceneManager.Instance.researchInputPanel.InitPanelInput(data, ResearchStart, icon.sprite);
    }

    private void ResearchStart()
    {
        PersonPlayer player = MainSceneManager.Instance.GetPlayer();

        switch (data.Type)
        {
            case ResearchType.Warhead:
                if (player.UnlockedWarheadIdx.Contains(data.ResearchThingIdx))
                {
                    PanelException.CallPopupPanl("이미 끝난 탄두 연구입니다!", () => { });
                    return;
                }
                break;
            case ResearchType.Engine:
                if (player.ResearchedEngineResearch.Contains(data.Idx))
                {
                    PanelException.CallPopupPanl("이미 끝난 엔진 연구입니다!", () => { });
                    return;
                }
                break;
            case ResearchType.Body:
                if (player.ResearchedBodyResearch.Contains(data.Idx))
                {
                    PanelException.CallPopupPanl("이미 끝난 선체 연구입니다!", () => { });
                    return;
                }
                break;
            default:
                break;
        }

        if (player.CurResearchData != null && player.CurResearchData.Idx != -1)
        {
            PanelException.CallExecptionPanel("이미 연구중인 항목이 있습니다!\n진행 중인 연구가 초기화됩니다.", () => player.CurResearchData = data, "계속 진행", () => { }, "취소");
            return;
        }

        player.CurResearchData = data;
    }
}
