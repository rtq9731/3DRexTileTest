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
            Debug.Log("정보창 감지 불가!");
        }
    }

    private void TurnMe(TileScript tile)
    {
        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"소유주 : {ownerName}";
        groundTypeText.text = $"지형 : {tile.Data.type}";

        string info;
        switch (tile.Data.type)
        {
            case TileType.Ocean:
                info = "그냥 물입니다.\n경치가 참 좋네요.\n 물고기도 잡힌다죠?";
                break;
            case TileType.Lake:
                info = "그냥 연못입니다.\n경치가 참 좋네요.\n앉아서 쉬고싶네요.";
                break;
            case TileType.Forest:
                info = "여러 나무들로 둘러싸인 지형입니다.\n사거리 - 1\n방어력 + 1";
                break;
            case TileType.DigSite:
                info = "광산이 위치한 타일입니다.\n사거리 - 1\n자원 + 1";
                break;
            case TileType.Plain:
                info = "별다른 특징이 없는 평지 타일입니다.\n특이사항 없음";
                break;
            case TileType.Mountain:
                info = "산위에 올라갈 수 있는 언덕 타일입니다.\n사거리 + 1\n자원 - 1";
                break;
            default:
                info = "어.. 이 지형은 있으면 안되는데?";
                break;
        }

        InfoText.text = info;

        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        
    }
}
