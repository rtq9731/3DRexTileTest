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
        textPlayerName.text = $"�÷��̾� : {data.playerData.PlayerName}";
        textStage.text = $"�������� : {data.stageCount}";
        textTurn.text = $"����� �� : {data.turnCnt}";
    }

}
