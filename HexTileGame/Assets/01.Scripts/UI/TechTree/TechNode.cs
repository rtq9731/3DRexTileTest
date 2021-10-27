using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TechNode : MonoBehaviour
{
    [SerializeField] int idx;
    [SerializeField] Button myBtn = null;

    SkillTreeNode data = null;
    PlayerScript player = null;

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
            case ResearchType.Material:
                break;
            default:
                break;
        }
    }

    private void OnClickCallPanelInput()
    {
        MainSceneManager.Instance.researchInputPanel.InitPanelInput(data, ResearchStart);
    }

    private void ResearchStart()
    {
        if (MainSceneManager.Instance.GetPlayer().CurResearchData != null)
        {
            PanelException.CallExecptionPanel("이미 연구중인 항목이 있습니다!\n진행 중인 연구가 초기화됩니다.", () => MainSceneManager.Instance.GetPlayer().CurResearchData = data, "계속 진행", () => { }, "취소");
            return;
        }

        MainSceneManager.Instance.GetPlayer().CurResearchData = data;
    }
}
