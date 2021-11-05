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

        MainSceneManager.Instance.GetPlayer().TurnFinishAction += MainSceneManager.Instance.RerollStage;
    }
}
