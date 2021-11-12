using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BtnReroll : MonoBehaviour
{
    Button btnReroll;

    private void Start()
    {
        btnReroll = GetComponent<Button>();
        btnReroll.onClick.AddListener(MainSceneManager.Instance.RerollStage);
    }

    public void ActiveReroll()
    {
        btnReroll.interactable = true;
        GetComponent<RectTransform>().DOAnchorPosY(-40, 0.5f);
    }

    public void RemoveReroll()
    {
        if (!gameObject.activeSelf)
            return;

        btnReroll.interactable = false;
        GetComponent<RectTransform>().DOAnchorPosY(70, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
