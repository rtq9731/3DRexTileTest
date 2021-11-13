using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] Button btnSave;
    [SerializeField] Button btnLoad;
    [SerializeField] Button btnExit;

    private void Start()
    {
        btnExit.onClick.AddListener(Application.Quit);
        btnSave.onClick.AddListener(GameManager.Instance.SaveData);
        //btnLoad.onClick.AddListener(GameManager.Instance.LoadData);
    }
}
