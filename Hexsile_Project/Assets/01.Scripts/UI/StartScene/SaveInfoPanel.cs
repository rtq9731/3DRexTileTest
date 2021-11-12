using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveInfoPanel : MonoBehaviour
{
    [SerializeField] Button btnPlayInfo = null;
    [SerializeField] Text textPlayerName = null;
    [SerializeField] Text textStage = null;
    [SerializeField] Text textTurn = null;

    SaveDataInfoPanel infoPanel = null;
    SaveData data = null;
    
    public void InitInfoPanel(SaveData data, SaveDataInfoPanel infoPanel)
    {
        textPlayerName.text = $"플레이어 : {data.playerData.PlayerName}";
        textStage.text = $"스테이지 : {data.stageCount}";
        textTurn.text = $"진행된 턴 : {data.turnCnt}";
    }

}
