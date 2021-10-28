using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerScript
{
    private void Start()
    {
        base.Start();
        MainSceneManager.Instance.AIPlayers.Add(this);
    }
}
