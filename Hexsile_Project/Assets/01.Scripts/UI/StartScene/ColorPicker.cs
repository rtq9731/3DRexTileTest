using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] public Color[] colorSet;

    [SerializeField] Button btnLeft;
    [SerializeField] Button btnRight;
    [SerializeField] Text sampleText;

    int colorNum = 0;

    private void Start()
    {
        GameManager.Instance.colorSet = colorSet;
        sampleText.color = colorSet[colorNum];
        sampleText.text = $"»ö»ó »ùÇÃ {colorNum + 1}";

        btnLeft.onClick.AddListener(() =>
        {
            SetColor(-1);
        });

        btnRight.onClick.AddListener(() =>
        {
            SetColor(1);
        });

        GameManager.Instance.colorSet = colorSet;
    }

    public void SetColor(int num)
    {
        colorNum += num;
        colorNum = Mathf.Clamp(colorNum, 0, colorSet.Length - 1);

        sampleText.color = colorSet[colorNum];
        sampleText.text = $"»ö»ó »ùÇÃ {colorNum + 1}";
    }

    public Color GetColor()
    {
        return colorSet[colorNum];
    }
}
