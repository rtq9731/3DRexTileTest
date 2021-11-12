using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageChange : MonoBehaviour
{
    [SerializeField] RectTransform leftWing;
    [SerializeField] RectTransform rightWing;

    private Vector2 originLeftWingPos = Vector2.zero;
    private Vector2 originRightWingPos = Vector2.zero;

    public void CallStageChangePanel(float duration)
    {
        gameObject.SetActive(true);

        originLeftWingPos = leftWing.anchoredPosition;
        originRightWingPos = rightWing.anchoredPosition;

        leftWing.DOAnchorPosX(0, duration).SetEase(Ease.OutBounce);
        rightWing.DOAnchorPosX(0, duration).SetEase(Ease.OutBounce);  
    }

    public void RemoveStageChangePanel(float duration)
    {
        leftWing.DOAnchorPos(originLeftWingPos, duration);
        rightWing.DOAnchorPos(originRightWingPos, duration).OnComplete(() => gameObject.SetActive(false));
    }
}
