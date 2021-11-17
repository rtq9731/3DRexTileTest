using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextNoticeSave : MonoBehaviour
{
    Vector2 originPos = Vector2.zero;

    Tween t = null;

    private void Awake()
    {
        originPos = transform.position;
    }

    public void Saved()
    {
        gameObject.transform.position = originPos;
        gameObject.GetComponent<Text>().color = Color.black;

        if (t != null)
        {
            DOTween.Complete(t);
        }

        Sequence seq = DOTween.Sequence();

        seq.Append(gameObject.GetComponent<RectTransform>().DOAnchorPosY(1500, 1f).SetEase(Ease.InBack));
        seq.Join(gameObject.GetComponent<Text>().DOFade(0, 1f));

        t = seq;
    }
}
