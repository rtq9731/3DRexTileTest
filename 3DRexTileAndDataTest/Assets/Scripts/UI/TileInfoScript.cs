using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoScript : MonoBehaviour
{
    [SerializeField] Text ownerText = null;
    [SerializeField] Text groundTypeText = null;
    [SerializeField] Text InfoText = null;
    [SerializeField] Button btnBuy = null;
    [SerializeField] Button btnMoreInfo = null;

    public static List<TileScript> tiles = new List<TileScript>();

    public TileScript selectedTile = null;

    public static void TurnOnTileInfoPanel(TileScript tile)
    {
        
        if (GameManager.Instance.InfoPanel != null)
        {
            GameManager.Instance.InfoPanel.TurnOnMe(tile);
        }
    }

    public void RefreshTexts(TileScript tile)
    {

        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"������ : {ownerName}";
        groundTypeText.text = $"���� : {tile.Data.type}";

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                btnBuy.interactable = false;
                info = "�׳� ���Դϴ�.\n��ġ�� �� ���׿�.\n����⵵ ��������?";
                break;
            case TileType.Lake:
                btnBuy.interactable = false;
                info = "�׳� �����Դϴ�.\n��ġ�� �� ���׿�.\n�ɾƼ� ����ͳ׿�.";
                break;
            case TileType.Forest:
                btnBuy.interactable = true;
                info = "���� ������� �ѷ����� �����Դϴ�.\n��Ÿ� - 1\n���� + 1";
                break;
            case TileType.DigSite:
                btnBuy.interactable = true;
                info = "������ ��ġ�� Ÿ���Դϴ�.\n��Ÿ� - 1\n�ڿ� + 1";
                break;
            case TileType.Plain:
                btnBuy.interactable = true;
                info = "���ٸ� Ư¡�� ���� ���� Ÿ���Դϴ�.\nƯ�̻��� ����";
                break;
            case TileType.Mountain:
                btnBuy.interactable = true;
                info = "������ �ö� �� �ִ� ��� Ÿ���Դϴ�.\n��Ÿ� + 1\n�ڿ� - 1";
                break;
            default:
                info = "��.. �� ������ ������ �ȵǴµ�?";
                break;
        }
        InfoText.text = info;
    }

    private void TurnOnMe(TileScript tile)
    {
        if (selectedTile != null)
        selectedTile.RemoveSelect(GameManager.Instance.tileVcam);

        FindObjectOfType<CameraMove>().enabled = false;
        selectedTile = tile;
        selectedTile.SelectTile(GameManager.Instance.tileVcam);

        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"������ : {ownerName}";
        groundTypeText.text = $"���� : {tile.Data.type}";

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                btnBuy.interactable = false;
                info = "�׳� ���Դϴ�.\n��ġ�� �� ���׿�.\n����⵵ ��������?";
                break;
            case TileType.Lake:
                btnBuy.interactable = false;
                info = "�׳� �����Դϴ�.\n��ġ�� �� ���׿�.\n�ɾƼ� ����ͳ׿�.";
                break;
            case TileType.Forest:
                btnBuy.interactable = true;
                info = "���� ������� �ѷ����� �����Դϴ�.\n��Ÿ� - 1\n���� + 1";
                break;
            case TileType.DigSite:
                btnBuy.interactable = true;
                info = "������ ��ġ�� Ÿ���Դϴ�.\n��Ÿ� - 1\n�ڿ� + 1";
                break;
            case TileType.Plain:
                btnBuy.interactable = true;
                info = "���ٸ� Ư¡�� ���� ���� Ÿ���Դϴ�.\nƯ�̻��� ����";
                break;
            case TileType.Mountain:
                btnBuy.interactable = true;
                info = "������ �ö� �� �ִ� ��� Ÿ���Դϴ�.\n��Ÿ� + 1\n�ڿ� - 1";
                break;
            default:
                info = "��.. �� ������ ������ �ȵǴµ�?";
                break;
        }

        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() => {
            tile.BuyTile(GameManager.Instance.Players.Find(x => x.MyName == GameManager.Instance.PlayerName));
            btnBuy.onClick.RemoveAllListeners();
            });
        InfoText.text = info;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (selectedTile != null)
        {
            FindObjectOfType<CameraMove>().enabled = true;
            selectedTile.RemoveSelect(GameManager.Instance.tileVcam);
        }
    }
}
