using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearPanel : MonoBehaviour
{
    [SerializeField] Button btnExit;
    [SerializeField] Button btnNextStage;

    public void Awake()
    {
        btnExit.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            GameManager.Instance.GoMainMenu();
        });
    }

    public void CallStageClearPanel(Action callBack)
    {
        btnNextStage.onClick.RemoveAllListeners();

        transform.parent.gameObject.SetActive(true);
        btnNextStage.onClick.AddListener(() => callBack());
        btnNextStage.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
    }
}
