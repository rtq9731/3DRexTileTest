using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Clock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] RectTransform longHand;
    [SerializeField] RectTransform shrotHand;
     
    PointerEventData eventData;

    float longHandRotation = 0;
    float shortHandRotation = 0;

    bool isPointerOn = false;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.OnSelect(eventData);
    }

    private void Update()
    {
        if(isPointerOn)
        {
            longHandRotation -= Time.deltaTime * 240;
            longHand.rotation = Quaternion.Euler(new Vector3(0, 0, longHandRotation));
            shortHandRotation -= Time.deltaTime * 20;
            shrotHand.rotation = Quaternion.Euler(new Vector3(0, 0, shortHandRotation));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOn = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isPointerOn = false;
    }
}
