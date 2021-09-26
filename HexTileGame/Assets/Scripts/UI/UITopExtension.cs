using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopExtension : MonoBehaviour
{
    [Header("연결 해야하는 버튼")]
    [SerializeField] Button btnMissile;   

    [Header("연결 되는 패널")]
    [SerializeField] GameObject panelMissile;

    private void Awake()
    {
        btnMissile.onClick.AddListener(() => panelMissile.SetActive(true));
    }
}
