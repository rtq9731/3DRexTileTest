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
                "저장 없이 메인 메뉴로 돌아갑니다.\n진행 사항이 저장되지 않을 것입니다.",
                () => GameManager.Instance.GoMainMenu(),
                "확인",
                () => UIStackManager.RemoveUIOnTop(),
                "취소");
        });

        btnMenuWithSave.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            GameManager.Instance.GoMainMenu();
        });

        btnExit.onClick.AddListener(() =>
        {
            PanelException.CallExecptionPanel(
                "저장 없이 게임을 종료합니다.\n진행 사항이 저장되지 않을 것입니다.",
                () => Application.Quit(),
                "확인",
                () => UIStackManager.RemoveUIOnTop(),
                "취소");
        });

        btnExitWithSave.onClick.AddListener(() =>
        {
            GameManager.Instance.SaveData();
            Application.Quit();
        });
    }
}
