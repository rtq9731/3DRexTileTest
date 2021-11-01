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
        btnReroll.onClick.AddListener(OnClickReroll);

        btnReroll.gameObject.GetComponent<RectTransform>().DOAnchorPosY(-32f, 0.3f);
    }

    public void OnClickReroll()
    {
        if (!MainSceneManager.Instance.CanReroll())
        {
            btnReroll.onClick.RemoveAllListeners();
            return;
        }

        MainSceneManager.Instance.tileGenerator.GenerateNewTileWihtNoExtension();
    }
}
