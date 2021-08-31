using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoScript : MonoBehaviour
{
    [SerializeField] Text ownerText;
    [SerializeField] Text groundTypeText;
    [SerializeField] Text InfoText;

    public static List<TileScript> tiles = new List<TileScript>();

    public static void TurnOnTileInfoPanel(TileScript tile)
    {
        
        if (GameManager.Instance.InfoPanel != null)
        {
            GameManager.Instance.InfoPanel.TurnMe(tile);
        }
        else
        {
            Debug.Log("����â ���� �Ұ�!");
        }
    }

    private void TurnMe(TileScript tile)
    {
        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"������ : {ownerName}";
        groundTypeText.text = $"���� : {tile.Data.type}";

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                info = "�׳� ���Դϴ�.\n��ġ�� �� ���׿�.\n ����⵵ ��������?";
                break;
            case TileType.Lake:
                info = "�׳� �����Դϴ�.\n��ġ�� �� ���׿�.\n�ɾƼ� ����ͳ׿�.";
                break;
            case TileType.Forest:
                info = "���� ������� �ѷ����� �����Դϴ�.\n��Ÿ� - 1\n���� + 1";
                break;
            case TileType.DigSite:
                info = "������ ��ġ�� Ÿ���Դϴ�.\n��Ÿ� - 1\n�ڿ� + 1";
                break;
            case TileType.Plain:
                info = "���ٸ� Ư¡�� ���� ���� Ÿ���Դϴ�.\nƯ�̻��� ����";
                break;
            case TileType.Mountain:
                info = "������ �ö� �� �ִ� ��� Ÿ���Դϴ�.\n��Ÿ� + 1\n�ڿ� - 1";
                break;
            default:
                info = "��.. �� ������ ������ �ȵǴµ�?";
                break;
        }

        InfoText.text = info;

        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        
    }
}
