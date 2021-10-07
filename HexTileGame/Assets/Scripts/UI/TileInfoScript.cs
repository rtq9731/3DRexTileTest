using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoScript : MonoBehaviour
{
    [SerializeField] Image tileIcon = null;
    [SerializeField] Text ownerText = null;
    [SerializeField] Text groundTypeText = null;
    [SerializeField] Text InfoText = null;
    [SerializeField] Button btnBuy = null;

    public static List<TileScript> tiles = new List<TileScript>();

    public TileScript selectedTile = null;

    public static void TurnOnTileInfoPanel(TileScript tile)
    {
        
        if (MainSceneManager.Instance.InfoPanel != null)
        {
            MainSceneManager.Instance.InfoPanel.TurnOnMe(tile);
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
                tileIcon.sprite = Resources.Load("/TileIcon/WaterIcon") as Sprite;
                btnBuy.interactable = false;
                info = "�׳� ���Դϴ�.\n��ġ�� �� ���׿�.\n����⵵ ��������?";
                break;
            case TileType.Lake:
                tileIcon.sprite = Resources.Load("/TileIcon/WaterIcon") as Sprite;
                btnBuy.interactable = false;
                info = "�׳� �����Դϴ�.\n��ġ�� �� ���׿�.\n�ɾƼ� ����ͳ׿�.";
                break;
            case TileType.Forest:
                tileIcon.sprite = Resources.Load("/TileIcon/ForestIcon") as Sprite;
                btnBuy.interactable = true;
                info = "���� ������� �ѷ����� �����Դϴ�.\n��Ÿ� - 1\n���� + 1";
                break;
            case TileType.DigSite:
                tileIcon.sprite = Resources.Load("/TileIcon/MineIcon") as Sprite;
                btnBuy.interactable = true;
                info = "������ ��ġ�� Ÿ���Դϴ�.\n��Ÿ� - 1\n�ڿ� + 1";
                break;
            case TileType.Plain:
                tileIcon.sprite = Resources.Load("/TileIcon/FieldIcon") as Sprite;
                btnBuy.interactable = true;
                info = "���ٸ� Ư¡�� ���� ���� Ÿ���Դϴ�.\nƯ�̻��� ����";
                break;
            case TileType.Mountain:
                tileIcon.sprite = Resources.Load("/TileIcon/MoutainIcon") as Sprite;
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
        selectedTile.RemoveSelect(MainSceneManager.Instance.tileVcam);

        FindObjectOfType<CameraMove>().enabled = false;
        selectedTile = tile;
        selectedTile.SelectTile(MainSceneManager.Instance.tileVcam);

        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"������ : {ownerName}";
        groundTypeText.text = $"���� : {tile.Data.type}";

        var a = Resources.Load<Sprite>("TileIcon/WaterIcon");
        Debug.Log(a);

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/WaterIcon");
                btnBuy.interactable = false;
                info = "�׳� ���Դϴ�.\n��ġ�� �� ���׿�.\n����⵵ ��������?";
                break;
            case TileType.Lake:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/WaterIcon");
                btnBuy.interactable = false;
                info = "�׳� �����Դϴ�.\n��ġ�� �� ���׿�.\n�ɾƼ� ����ͳ׿�.";
                break;
            case TileType.Forest:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/ForestIcon");
                btnBuy.interactable = true;
                info = "���� ������� �ѷ����� �����Դϴ�.\n��Ÿ� - 1\n���� + 1";
                break;
            case TileType.DigSite:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/MineIcon");
                btnBuy.interactable = true;
                info = "������ ��ġ�� Ÿ���Դϴ�.\n��Ÿ� - 1\n�ڿ� + 1";
                break;
            case TileType.Plain:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/FieldIcon");
                btnBuy.interactable = true;
                info = "���ٸ� Ư¡�� ���� ���� Ÿ���Դϴ�.\nƯ�̻��� ����";
                break;
            case TileType.Mountain:
                tileIcon.sprite = Resources.Load<Sprite>("TileIcon/MoutainIcon");
                btnBuy.interactable = true;
                info = "������ �ö� �� �ִ� ��� Ÿ���Դϴ�.\n��Ÿ� + 1\n�ڿ� - 1";
                break;
            default:
                info = "��.. �� ������ ������ �ȵǴµ�?";
                break;
        }

        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() => {
            tile.BuyTile(MainSceneManager.Instance.Players.Find(x => x.MyName == MainSceneManager.Instance.PlayerName));
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
            selectedTile.RemoveSelect(MainSceneManager.Instance.tileVcam);
        }
    }
}
