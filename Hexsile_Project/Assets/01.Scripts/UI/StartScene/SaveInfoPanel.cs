using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveInfoPanel : MonoBehaviour
{
    [SerializeField] Button btnPlayInfo = null;
    [SerializeField] Text textSaveDate = null;
    [SerializeField] Text textPlayerName = null;
    [SerializeField] Text textStage = null;
    [SerializeField] Text textTurn = null;

    SaveDataInfoPanel infoPanel = null;
    public SaveData data;
    
    public void InitInfoPanel(SaveData data)
    {
        textSaveDate.text = data.saveTime.ToString("yyyy-MM-dd");
        textPlayerName.text = $"�÷��̾� : {data.playerData.PlayerName}";
        textStage.text = $"�������� : {data.stageCount}";
        textTurn.text = $"����� �� : {data.turnCnt}";
        this.data = data;
        this.infoPanel = FindObjectOfType<SaveDataInfoPanel>();

        btnPlayInfo.onClick.AddListener(() => infoPanel.UpdateSaveDataInfo(data));
    }

}
