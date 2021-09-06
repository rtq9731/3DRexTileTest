using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPlayer : PlayerScript
{
    [SerializeField] string playerName;

    private void Start()
    {
        GameManager.Instance.PlayerName = playerName;
        base.myName = playerName;
        base.Start();
    }

}