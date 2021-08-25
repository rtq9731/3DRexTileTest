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
        DOTween.Kill(this.gameObject.transform.GetComponent<Image>());
        gameObject.transform.position = Input.mousePosition;
        gameObject.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        gameObject.SetActive(true);

        TileData data = tile.Data;

        gameObject.transform.GetComponent<Image>().DOFade(1, 0.3f).SetEase(Ease.OutQuad);

        string ownerName = tile.Owner != null ? tile.Owner.OwnerName : "None";
        ownerText.text = $"������ : " + ownerName;
        groundTypeText.text = $"���� : {data.type}";
        priceText.text = $"���� : {data.price}";
        rangeText.text = $"��Ÿ� : {data.range}";
        shieldText.text = $"���� : {data.shield}";
        attackPowerText.text = $"���ݷ� : {data.attackPower}";
        productText.text = $"���� : {data.resource}";
    }

    public void RemoveSimpleTileInfoPanel()
    {
        gameObject.transform.GetComponent<Image>().DOFade(0, 0.3f).SetEase(Ease.InQuint).OnComplete(() => gameObject.SetActive(false));
    }
}
