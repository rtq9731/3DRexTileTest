using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechPanel : MonoBehaviour
{
    [SerializeField] GameObject uiVcam = null;
    [SerializeField] GameObject uiCam = null;
    [SerializeField] ScrollRect scrollView = null;

    private void OnEnable()
    {
        scrollView.content.anchoredPosition = Vector2.zero;
    }

    private void OnDisable()
    {
        if(uiVcam != null)
        {
            uiVcam.SetActive(false);
        }
        
        if(uiCam != null)
        {
            uiCam.SetActive(false);
        }
    }

}
