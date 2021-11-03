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
    [SerializeField] float alphaOnTime = 0.5f;

    private Image myImage = null;

    TileScript lastSelectedTile = null;

    private float onTime = 0f;

    public void CallSimpleTileInfoPanel(TileScript tile)
    {
        DOTween.Kill(myImage);
        
        if(tile != lastSelectedTile)
        {
            lastSelectedTile = tile;
            onTime = 0f;
        }

        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        gameObject.transform.position = Input.mousePosition;
        gameObject.transform.position += new Vector3(4f, 0, 0);

        onTime += Time.deltaTime;

        if (myImage == null)
        {
            myImage = GetComponent<Image>();
        }

        myImage.color = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, onTime / alphaOnTime));

        string ownerName = tile.Owner != null ? tile.Owner.MyName : "None";

        TileData data = tile.Data;

        ownerText.text = $"������ : {ownerName}";
        groundTypeText.text = $"���� : {data.type}";
        priceText.text = $"���� : {data.Price}";
        rangeText.text = $"��Ÿ� : {data.Range}";
        shieldText.text = $"���� : {data.Shield}";
        attackPowerText.text = $"���ݷ� : {data.AttackPower}";
        productText.text = $"���� : {data.Resource}";
    }

    public void RemoveSimpleTileInfoPanel()
    {
        if (!gameObject.activeSelf)
            return;

        myImage.DOFade(0, 0.3f).SetEase(Ease.InQuint).OnComplete(() => gameObject.SetActive(false));
    }
}
