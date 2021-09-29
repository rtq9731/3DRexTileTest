using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SimpleTileInfoPanel : MonoBehaviour
{
    [SerializeField] Text ownerText;
    [SerializeField] Text groundTypeText;
    [SerializeField] Text priceText;
    [SerializeField] Text rangeText;
    [SerializeField] Text shieldText;
    [SerializeField] Text attackPowerText;
    [SerializeField] Text productText;    

    public void CallSimpleTileInfoPanel(TileScript tile)
    {
        DOTween.Kill(this.gameObject);
        gameObject.transform.position = Input.mousePosition;
        gameObject.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        gameObject.SetActive(true);

        TileData data = tile.Data;

        gameObject.transform.GetComponent<Image>().DOFade(1, 0.3f).SetEase(Ease.OutQuad);

        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";
        ownerText.text = $"소유자 : {ownerName}";
        groundTypeText.text = $"지형 : {data.type}";
        priceText.text = $"가격 : {data.Price}";
        rangeText.text = $"사거리 : {data.Range}";
        shieldText.text = $"방어력 : {data.Shield}";
        attackPowerText.text = $"공격력 : {data.AttackPower}";
        productText.text = $"생산 : {data.Resource}";
    }

    public void RemoveSimpleTileInfoPanel()
    {
        if (!gameObject.activeSelf)
            return;

        gameObject.transform.GetComponent<Image>().DOFade(0, 0.3f).SetEase(Ease.InQuint).OnComplete(() => gameObject.SetActive(false));
    }
}
