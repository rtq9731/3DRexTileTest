using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPlayer : PlayerScript
{
    [Header("�÷��̾� ���� �Է�")]
    [SerializeField] string playerName;
    [SerializeField] Color color;

    private void Start()
    {
        Debug.Log("����");
        MainSceneManager.Instance.PlayerName = playerName;
        base.playerColor = color;
        base.myName = playerName;
        base.Start();
    }

}
