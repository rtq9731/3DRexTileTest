using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelException : MonoBehaviour
{
    static PanelException instance = null;
    [SerializeField] Text textMsg = null;
    [SerializeField] Button btnOK = null;
    [SerializeField] Text textBtnOk = null;
    [SerializeField] Button btnCancel = null;
    [SerializeField] Text textBtnCancel = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static void CallPopupPanl(object errorMsg, Action trueAct)
    {
        instance.btnOK.gameObject.SetActive(true);
        instance.btnCancel.gameObject.SetActive(false);

        instance.gameObject.transform.parent.gameObject.SetActive(true);
        instance.textMsg.text = errorMsg.ToString();

        instance.textBtnOk.text = "È®ÀÎ";
        instance.btnOK.onClick.RemoveAllListeners();
        trueAct += instance.OnClickRemovePanel;
        instance.btnOK.onClick.AddListener(() => trueAct());
    }

    public static void CallExecptionPanel(object errorMsg, Action trueAct, string trueMsg , Action falseAct, string falseMsg)
    {
        instance.btnOK.gameObject.SetActive(true);
        instance.btnCancel.gameObject.SetActive(true);

        instance.gameObject.transform.parent.gameObject.SetActive(true);
        instance.textMsg.text = errorMsg.ToString();

        instance.textBtnOk.text = trueMsg;
        instance.btnOK.onClick.RemoveAllListeners();
        trueAct += instance.OnClickRemovePanel;
        instance.btnOK.onClick.AddListener(() => trueAct());

        instance.textBtnCancel.text = falseMsg;
        instance.btnCancel.onClick.RemoveAllListeners();
        falseAct += instance.OnClickRemovePanel;
        instance.btnCancel.onClick.AddListener(() => falseAct());
    }

    private void OnClickRemovePanel()
    {
        UIStackManager.RemoveUIOnTop();
    }

}
