using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataInfoPanel : MonoBehaviour
{

    [SerializeField] Button btnLoad = null;
    [SerializeField] Text textPlayerName = null;
    [SerializeField] Text textPlayerResource = null;
    [SerializeField] Text textFireableMissiles = null;
    [SerializeField] Text textSaveDateTime = null;
    [SerializeField] Text textStage = null;
    [SerializeField] Image playerColorImage = null;

    void Start()
    {
        textPlayerName.text = "���̺����� ���õ��� ����!";
        textPlayerResource.text = $"���� ���� �ڿ� : ";
        textFireableMissiles.text = $"�߻� ������ �̻��� �� : ";
        textSaveDateTime.text = $"����� �ð� : ";
        textStage.text = $"�������� : ";
        playerColorImage.color = Color.white;
    }

    public void UpdateSaveDataInfo(SaveData data)
    {
        textPlayerName.text = data.playerData.PlayerName;
        textPlayerResource.text = $"���� ���� �ڿ� : {data.playerData.ResourceTank}";
        textFireableMissiles.text = $"�߻� ������ �̻��� �� : {data.playerData.MissileReadyToShoot.Count}";
        textSaveDateTime.text = $"����� �ð� : {data.saveTime.ToString("yyyy-MM-dd")}";
        textStage.text = $"�������� : {data.stageCount}";
        playerColorImage.color = data.playerData.PlayerColor;
    }
}
