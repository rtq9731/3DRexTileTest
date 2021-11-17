using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] Button btnSave;
    [SerializeField] Button btnMenu;
    [SerializeField] Button btnExit;

    private void Start()
    {
        btnSave.onClick.AddListener(GameManager.Instance.SaveData);
        btnMenu.onClick.AddListener(GameManager.Instance.GoMainMenu);
        btnExit.onClick.AddListener(Application.Quit);
    }
}
