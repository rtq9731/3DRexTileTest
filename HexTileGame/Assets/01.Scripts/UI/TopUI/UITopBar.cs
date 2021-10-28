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

    PersonPlayer player;

    public void UpdateTexts()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        resourceText.text = $"{player.ResourceTank}";
        turnCntText.text = $"ео : {MainSceneManager.Instance.turnCnt}";
        missileText.text = $"{player.MissileInMaking.Count + player.MissileReadyToShoot.Count} / {player.OwningTiles.Count}";
    }
}
