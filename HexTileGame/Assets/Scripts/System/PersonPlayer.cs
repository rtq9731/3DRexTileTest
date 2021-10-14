using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPlayer : PlayerScript
{
    [Header("�÷��̾� ���� �Է�")]
    [SerializeField] string playerName;
    [SerializeField] Color color;

    List<int> unlockedWarheadIdx = new List<int>();
    List<int> unlockedEngineIdx = new List<int>();

    private void Start()
    {
        MainSceneManager.Instance.PlayerName = playerName;
        base.playerColor = color;
        base.myName = playerName;
        base.Start();
    }

}
