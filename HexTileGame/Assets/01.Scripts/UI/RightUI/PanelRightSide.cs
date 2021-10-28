using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRightSide : MonoBehaviour
{
    [SerializeField] Button btnFinishTurn;
    PlayerScript player = null;

    private void Start()
    {
        btnFinishTurn.onClick.AddListener(() => FinishTurn());
    }

    public void FinishTurn()
    {
        if(!UIStackManager.IsUIStackEmpty())
        {
            return;
        }

        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        player.TurnFinish();
    }
}
