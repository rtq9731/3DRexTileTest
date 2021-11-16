using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Button btnMainMenu = null;

    private void Start()
    {
        btnMainMenu.onClick.AddListener(GameManager.Instance.GoMainMenu);
    }
}
