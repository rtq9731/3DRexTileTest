using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopExtension : MonoBehaviour
{
    [Header("���� �ؾ��ϴ� ��ư")]
    [SerializeField] Button btnFireMissile;
    [SerializeField] Button btnReserchMissile;
    [SerializeField] Button btnMakeMissile;
    [SerializeField] Button btnMenu;

    [Header("���� �Ǵ� �г�")]
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
