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

        btnReroll.onClick.AddListener(() => MainSceneManager.Instance.RerollStage(btnReroll));
        MainSceneManager.Instance.GetPlayer().TurnFinishAction += () => gameObject.SetActive(false);
    }
}
