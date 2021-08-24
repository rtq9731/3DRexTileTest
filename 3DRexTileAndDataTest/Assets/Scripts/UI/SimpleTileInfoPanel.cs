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
        DOTween.Complete(this.gameObject);
        gameObject.SetActive(true);

        gameObject.transform.position = Input.mousePosition;

        gameObject.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        TileData data = tile.Data;

        gameObject.transform.GetComponent<Image>().DOFade(1, 0.3f).SetEase(Ease.OutQuad);
            // 키고나서 천천히 꺼줌.

        //ownerText.text = $"소유자 : {tile.Owner.OwnerName}";
        groundTypeText.text = $"지형 : {data.type}";
        priceText.text = $"가격 : {data.price}";
        rangeText.text = $"사거리 : {data.range}";
        shieldText.text = $"방어력 : {data.shield}";
        attackPowerText.text = $"공격력 : {data.attackPower}";
        productText.text = $"생산 : {data.resource}";
    }

    public void RemoveSimpleTileInfoPanel()
    {
        gameObject.transform.GetComponent<Image>().DOFade(0, 0.3f).SetEase(Ease.InQuint).OnComplete(() => gameObject.SetActive(false));
    }
}
