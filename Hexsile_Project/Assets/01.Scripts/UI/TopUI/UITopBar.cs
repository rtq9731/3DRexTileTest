using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITopBar : MonoBehaviour
{
    [SerializeField] Text resourceText = null;
    [SerializeField] Text turnCntText = null;
    [SerializeField] Text missileText = null;
    [SerializeField] Text stageText = null;

    PersonPlayer player;

    public void UpdateTexts()
    {
        if(player == null)
        {
            player = MainSceneManager.Instance.GetPlayer();
        }

        resourceText.text = $"{player.ResourceTank}";
        turnCntText.text = $"턴 : {MainSceneManager.Instance.turnCnt}";
        missileText.text = $"{player.MissileInMaking.Count + player.MissileReadyToShoot.Count} / {player.OwningTiles.Count}";
        stageText.text = $"현재 스테이지 : {MainSceneManager.Instance.stageCount}";
    }
}
