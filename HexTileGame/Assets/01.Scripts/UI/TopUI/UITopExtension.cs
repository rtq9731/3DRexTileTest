using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopExtension : MonoBehaviour
{
    [Header("연결 해야하는 버튼")]
    [SerializeField] Button btnFireMissile;
    [SerializeField] Button btnReserchMissile;
    [SerializeField] Button btnMakeMissile;
    [SerializeField] Button btnMenu;

    [Header("연결 되는 패널")]
    [SerializeField] GameObject panelFireMissile;
    [SerializeField] GameObject panelReserchMissile;
    [SerializeField] GameObject panelMakeMissile;
    [SerializeField] GameObject panelMenu;

    private void Awake()
    {
        btnFireMissile.onClick.AddListener(() =>
        {
            if (UIStackManager.IsUIStackEmpty())
                panelFireMissile.SetActive(true);
        });

        btnReserchMissile.onClick.AddListener(() => 
        {
            if (UIStackManager.IsUIStackEmpty())
                panelReserchMissile.SetActive(true);
        });

        btnMakeMissile.onClick.AddListener(() => 
        {
            if (UIStackManager.IsUIStackEmpty())
                panelMakeMissile.SetActive(true);
        });

        btnMenu.onClick.AddListener(() =>
        {
            if (UIStackManager.IsUIStackEmpty())
                panelMenu.SetActive(true);
        });
    }
}
