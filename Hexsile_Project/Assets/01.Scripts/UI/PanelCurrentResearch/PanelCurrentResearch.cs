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
            textResearchName.text = "현재 연구중 : 연구중인 항목 없음.";
            textResearchComplete.text = "연구 완료까지 : 없음";
            textNowResearchStatus.text = "현재 연구중인 항목 없음";
            return;
        }

        textResearchName.text = $"현재 연구중 : {player.CurResearchData.ResearchInfo}";
        textResearchComplete.text = $"연구 완료까지 {player.ResearchFinishTurn} 턴";
        textNowResearchStatus.text = $"현재 연구중인 항목이 {player.ResearchFinishTurn} 턴 후 완료";
        
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
