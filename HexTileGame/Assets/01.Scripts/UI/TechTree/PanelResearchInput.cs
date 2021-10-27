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
    /// <param name="callBack">������� �Է��� �����ݴϴ�</param>
    public void InitPanelInput(SkillTreeNode data, Action callBack)
    {
        gameObject.SetActive(true);
        RefreshTexts();

        switch (data.Type)
        {
            case ResearchType.Warhead:
                MissileWarheadData warheadData = MainSceneManager.Instance.GetWarheadByIdx(data.ResearchThingIdx);
                textResearchName.text = warheadData.Name;
                textWeight.text = $"ź�� ���� : {warheadData.Weight}";
                textDamage.text = $"ź�� ���ݷ� : {warheadData.Atk}";
                textResearchInfo.text = $"{warheadData.Info}";
                break;
            case ResearchType.Engine:
                if (data.ResearchInfo.Contains("�ܰ�")) // ���� ��¥ ���� ������
                {
                    MissileEngineData engineData = MainSceneManager.Instance.GetEngineDataByIdx(data.ResearchThingIdx);
                    textResearchName.text = engineData.Name;
                    textWeight.text = $"���� ������ ���� : {engineData.Weight}";
                    textDamage.gameObject.SetActive(false);
                }
                else if (data.ResearchInfo.Contains("����")) // ���� ���� ������ ���� ������
                {

                }
                else if(data.ResearchInfo.Contains("����")) // ���� ���� ������ ȿ�� ������
                {

                }
                break;
            case ResearchType.Material:
                break;
            default:
                break;
        }

        textResearchTime.text = $"�ҿ� �ð� : {data.TrunForResearch} ��";
        textResource.text = $"������ �Ҹ�Ǵ� �ڿ� : {data.ResearchResource}";

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
        textResource.gameObject.SetActive(true);
        textResearchInfo.gameObject.SetActive(true);
    }
}
