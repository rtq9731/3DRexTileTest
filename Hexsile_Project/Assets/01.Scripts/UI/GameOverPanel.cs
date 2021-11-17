using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Button btnMainMenu = null;

    private void Awake()
    {
        btnMainMenu.onClick.AddListener(GameManager.Instance.GoMainMenu);
    }

    public void CallGameOverPanel()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
