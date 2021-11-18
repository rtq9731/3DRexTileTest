using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelCurrentResearch : MonoBehaviour
{
    [SerializeField] Text textResearchName = null;
    [SerializeField] Text textResearchComplete = null;
    [SerializeField] Text textNowResearchStatus = null;
    [SerializeField] Image researchIcon = null;
    [SerializeField] Sprite noneSprite = null;

    Toggle myToggle = null;
    RectTransform rectTr = null;
    float originY = 0f;

    private void Awake()
    {
        myToggle = GetComponentInChildren<Toggle>();
        rectTr = GetComponent<RectTransform>();
        originY = rectTr.anchoredPosition.y;

        myToggle.isOn = false;
        myToggle.onValueChanged.AddListener(OnToggleValueChanged);
        
    }
    private void Start()
    {
        UpdateTextsToNull();
    }

    public void UpdateTextsToNull()
    {
        researchIcon.sprite = noneSprite;
        textResearchName.text = "���� ������ : �������� �׸� ����.";
        textResearchComplete.text = "���� �Ϸ���� : ����";
        textNowResearchStatus.text = "���� �������� �׸� ����";
        return;
    }

    public void UpdateTexts(SkillTreeNode skill)
    {
        textResearchName.text = $"���� ������ : {skill.ResearchInfo}";
        textResearchComplete.text = $"���� �Ϸ���� {MainSceneManager.Instance.GetPlayer().ResearchFinishTurn} ��";
        textNowResearchStatus.text = $"���� �������� �׸��� {MainSceneManager.Instance.GetPlayer().ResearchFinishTurn} �� �� �Ϸ�";
    }

    public void SetTexts(SkillTreeNode skill, Sprite icon = null)
    {
        researchIcon.sprite = icon;

        textResearchName.text = $"���� ������ : {skill.ResearchInfo}";
        textResearchComplete.text = $"���� �Ϸ���� {MainSceneManager.Instance.GetPlayer().ResearchFinishTurn} ��";
        textNowResearchStatus.text = $"���� �������� �׸��� {MainSceneManager.Instance.GetPlayer().ResearchFinishTurn} �� �� �Ϸ�";
    }

    public void OnToggleValueChanged(bool isOn)
    {
        if(isOn)
        {
            rectTr.DOAnchorPosY(-32f, 0.3f);
        }
        else
        {
            rectTr.DOAnchorPosY(originY, 0.3f);
        }
    }
}
