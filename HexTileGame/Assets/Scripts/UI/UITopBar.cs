using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITopBar : MonoBehaviour
{
    [SerializeField] Text resourceText;
    [SerializeField] Text turnCntText;
    [SerializeField] Text missileText;

    PlayerScript player;

    public void UpdateTexts()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.Players.Find(x => x.MyName == MainSceneManager.Instance.PlayerName);
        }

        resourceText.text = $"{player.ResourceTank}";
        turnCntText.text = $"ео : {MainSceneManager.Instance.turnCnt}";
        missileText.text = $"{player.missiles.Count} / {player.owningTiles.Count}";
    }
}
