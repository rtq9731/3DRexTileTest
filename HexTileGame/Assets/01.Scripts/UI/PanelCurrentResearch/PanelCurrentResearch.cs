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
        UpdateTexts(null);
    }

    public void UpdateTexts(PersonPlayer player)
    {
        if(player == null || player.CurResearchData == null)
        {
            textResearchName.text = "���� ������ : �������� �׸� ����.";
            textResearchComplete.text = "���� �Ϸ���� : ����";
            textNowResearchStatus.text = "���� �������� �׸� ����";
            return;
        }

        textResearchName.text = $"���� ������ : {player.CurResearchData.ResearchInfo}";
        textResearchComplete.text = $"���� �Ϸ���� {player.ResearchFinishTurn} ��";
        textNowResearchStatus.text = $"���� �������� �׸��� {player.ResearchFinishTurn} �� �� �Ϸ�";
        
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
