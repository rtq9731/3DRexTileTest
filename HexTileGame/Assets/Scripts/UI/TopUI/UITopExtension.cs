using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopExtension : MonoBehaviour
{
    [Header("���� �ؾ��ϴ� ��ư")]
    [SerializeField] Button btnMissile;   

    [Header("���� �Ǵ� �г�")]
    [SerializeField] GameObject panelMissile;

    private void Awake()
    {
        btnMissile.onClick.AddListener(() => panelMissile.SetActive(true));
    }
}
