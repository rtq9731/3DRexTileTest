using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPlayer : PlayerScript
{
    [SerializeField] string playerName;
    [SerializeField] Color color;

    private void Start()
    {
        MainSceneManager.Instance.PlayerName = playerName;
        base.playerColor = color;
        base.myName = playerName;
        base.Start();
    }

}
