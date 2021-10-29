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
    /// <param name="callBack">������� �Է��� �����ݴϴ�</param>
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
                textWeight.text = $"ź�� ���� : {warheadData.Weight}";
                textDamage.text = $"ź�� ���ݷ� : {warheadData.Atk}";
                textResearchInfo.text = $"{warheadData.Info}";
                break;
            case ResearchType.Engine:
                if (data.ResearchInfo.Contains("�ܰ�")) // ���� ��¥ ���� ������
                {
                    Debug.Log(data.ResearchThingIdx);
                    MissileEngineData engineData = MainSceneManager.Instance.GetEngineDataByIdx(data.ResearchThingIdx);
                    textResearchName.text = engineData.Name;
                    textWeight.text = $"���� ������ ���� : {engineData.Weight}";
                }
                else if (data.ResearchInfo.Contains("����")) // ���� ���� ������ ���� ������
                {
                    textResearchName.text = "ȿ������ ����";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "�̻����� �� �� �Ÿ��� �ִ� ���� Ÿ�� �� �� �ֵ���,\n���� ����� ���ǰ� �ִ� ȭ�չ��� ������ �̸����� �ٲ㺾�ϴ�.\n�ٲ㺸�� �������� ���� ȿ������ ���ᰡ ���������,\n�츮 ���������� ������ �̻��� ������ ������ �� ���Դϴ�.";
                }
                else if(data.ResearchInfo.Contains("����")) // ���� ���� ������ ȿ�� ������
                {
                    textResearchName.text = "ȿ������ ����";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "�̻����� �� �� �Ÿ��� �ִ� ���� Ÿ�� �� �� �ֵ���,\n���� ������ ���ǰ� �ִ� ������� ������ ����� �ٲپ�ϴ�.\n������Ÿ���� �����ϴ� �������� ���� ȿ������ ������ ���������,\n�츮 ���������� ������ �̻��� ������ ������ �� ���Դϴ�.";
                }

                textDamage.gameObject.SetActive(false);
                break;
            case ResearchType.Body:
                if (data.ResearchInfo.Contains("����")) // ���� ��¥ ���� ������
                {
                    Debug.Log(data.ResearchThingIdx);
                    BodyData bodyData = MainSceneManager.Instance.GetMissileBodyByIdx(data.ResearchThingIdx);
                    textResearchName.text = bodyData.Name;
                    textWeight.text = $"�߰� ��Ÿ� : {bodyData.Morerange}";
                }
                else if (data.ResearchInfo.Contains("����")) // ���� ���� ������ ���� ������
                {
                    textResearchName.text = "�ռ� ���� ����";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "��ü�� �� ���� ������ ���� �� �� �ֵ���,\n���� ��ü ������ ���е��� �̸����� �ٲ㺾�ϴ�.\n�ٲ㺸�� �������� ���� ȿ������ ��ü�� ���������,\n�츮 ���������� ������ �̻��� ��ü�� ������ �� ���Դϴ�.";
                }
                else if (data.ResearchInfo.Contains("����")) // ���� ���� ������ ���� ������
                {
                    textResearchName.text = "���� �淮ȭ ����";
                    textWeight.gameObject.SetActive(false);
                    textResearchInfo.text = "�̻����� �� �� �Ÿ��� �ִ� ���� Ÿ�� �� �� �ֵ���,\n���� �̻����� ��ü�� ���Ǵ� �ݼ��� ��ü�غ��ϴ�.\n������Ÿ���� �����ϴ� �������� ���� ������ ��ü�� ���������,\n�츮 ���������� ������ �̻��� ��ü�� ������ �� ���Դϴ�.";
                }

                textDamage.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        textResearchTime.text = $"�ҿ� �ð� : {data.TrunForResearch} ��";

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
