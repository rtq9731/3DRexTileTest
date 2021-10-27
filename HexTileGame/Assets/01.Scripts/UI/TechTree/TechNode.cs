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

        switch (data.Type) // Ÿ�Կ� ���� �÷��̾ ���� �����ؾ��Ұ� ������ �ִ��� üũ
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
            PanelException.CallExecptionPanel("�̹� �������� �׸��� �ֽ��ϴ�!\n���� ���� ������ �ʱ�ȭ�˴ϴ�.", () => MainSceneManager.Instance.GetPlayer().CurResearchData = data, "��� ����", () => { }, "���");
            return;
        }

        MainSceneManager.Instance.GetPlayer().CurResearchData = data;
    }
}
