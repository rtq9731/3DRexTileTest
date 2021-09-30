using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRightSide : MonoBehaviour
{
    [SerializeField] Button btnFinishTurn;
    PlayerScript player = null;

    public void FinishTurn()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.Players.Find(x => x.MyName == MainSceneManager.Instance.PlayerName);
        }

        player.FinishTurn();
    }
}
