using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneBtns : MonoBehaviour
{
    [SerializeField] Button btnStartNewGame;
    [SerializeField] Button btnLoadGame;
    [SerializeField] Button btnExit;

    [SerializeField] GameObject panelNewGame;
    [SerializeField] GameObject panelLoadGame;

    private void Start()
    {
        btnStartNewGame.onClick.AddListener(() => panelNewGame.SetActive(true));
        btnLoadGame.onClick.AddListener(() => panelLoadGame.SetActive(true));
        btnExit.onClick.AddListener(() => Application.Quit());
    }
}
