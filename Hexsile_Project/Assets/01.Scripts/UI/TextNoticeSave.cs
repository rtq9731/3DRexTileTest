using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextNoticeSave : MonoBehaviour
{
    Vector2 originPos = Vector2.zero;

    Tween t = null;

    public void Saved()
    {
        if (t != null)
        {
            DOTween.Complete(t);
        }

        gameObject.gameObject.GetComponent<RectTransform>().anchoredPosition = originPos;
        gameObject.GetComponent<Text>().color = Color.black;


        Sequence seq = DOTween.Sequence();

        seq.Append(gameObject.GetComponent<RectTransform>().DOAnchorPosY(600, 0.75f).SetEase(Ease.InBack));
        seq.Join(gameObject.GetComponent<Text>().DOFade(0, 1f));

        t = seq;
    }
}
