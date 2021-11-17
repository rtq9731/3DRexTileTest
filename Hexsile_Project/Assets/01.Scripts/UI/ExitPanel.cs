using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] Button btnSave;
    [SerializeField] Button btnMenu;
    [SerializeField] Button btnMenuWithSave;
    [SerializeField] Button btnExit;
    [SerializeField] Button btnExitWithSave;

    private void Start()
    {
        btnSave.onClick.AddListener(() => 
        {
            GameManager.Instance.SaveData();
        });

        btnMenu.onClick.AddListener(() => 
        {
            PanelException.CallExecptionPanel(
                "���� ���� ���� �޴��� ���ư��ϴ�.\n���� ������ ������� ���� ���Դϴ�.",
                () => GameManager.Instance.GoMainMenu(),
                "Ȯ��",
                () => UIStackManager.RemoveUIOnTop(),
                "���");
        });

        btnMenuWithSave.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            GameManager.Instance.GoMainMenu();
        });

        btnExit.onClick.AddListener(() =>
        {
            PanelException.CallExecptionPanel(
                "���� ���� ������ �����մϴ�.\n���� ������ ������� ���� ���Դϴ�.",
                () => Application.Quit(),
                "Ȯ��",
                () => UIStackManager.RemoveUIOnTop(),
                "���");
        });

        btnExitWithSave.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            Application.Quit();
        });
    }
}
