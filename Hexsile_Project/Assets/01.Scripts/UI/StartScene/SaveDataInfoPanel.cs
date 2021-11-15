using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataInfoPanel : MonoBehaviour
{

    [SerializeField] Button btnLoad = null;
    [SerializeField] Button btnDelete = null;
    [SerializeField] Text textPlayerName = null;
    [SerializeField] Text textPlayerResource = null;
    [SerializeField] Text textFireableMissiles = null;
    [SerializeField] Text textSaveDateTime = null;
    [SerializeField] Text textStage = null;
    [SerializeField] Image playerColorImage = null;

    void Start()
    {
        UpdateSaveDataInfoToNull();
    }

    public void UpdateSaveDataInfoToNull()
    {
        textPlayerName.text = "세이브파일 선택되지 않음!";
        textPlayerResource.text = $"현재 가진 자원 : ";
        textFireableMissiles.text = $"발사 가능한 미사일 수 : ";
        textSaveDateTime.text = $"저장된 시각 : ";
        textStage.text = $"스테이지 : ";
        playerColorImage.color = Color.white;

        btnDelete.gameObject.SetActive(false);

        btnLoad.onClick.RemoveAllListeners();
        btnDelete.onClick.RemoveAllListeners();
    }

    public void UpdateSaveDataInfo(SaveData data)
    {
        textPlayerName.text = data.playerData.PlayerName;
        textPlayerResource.text = $"현재 가진 자원 : {data.playerData.ResourceTank}";
        textFireableMissiles.text = $"발사 가능한 미사일 수 : {data.playerData.MissileReadyToShoot.Count}";
        textSaveDateTime.text = $"저장된 시각 : {data.saveTime.ToString("yyyy-MM-dd HH:mm")}";
        textStage.text = $"스테이지 : {data.stageCount}";
        playerColorImage.color = data.playerData.PlayerColor;

        btnLoad.onClick.RemoveAllListeners();
        btnLoad.onClick.AddListener(() => GameManager.Instance.StartLoadedGame(data));

        btnDelete.gameObject.SetActive(true);
        btnDelete.onClick.RemoveAllListeners();
        btnDelete.onClick.AddListener(() =>
        {
            FindObjectsOfType<SaveInfoPanel>().ToList().FindAll(x => x.data == data).ForEach(x => Destroy(x.gameObject));
            UpdateSaveDataInfoToNull();
            GameManager.Instance.DeleteFile(data);
        });
    }
}
