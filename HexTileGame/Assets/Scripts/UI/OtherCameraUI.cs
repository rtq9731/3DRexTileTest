using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OtherCameraUI : MonoBehaviour
{
    [SerializeField] GameObject techPanel = null;
    [SerializeField] GameObject uiCam = null;

    private void Update()
    {
        if (Camera.main.transform != null && transform.position == Camera.main.transform.position) // 화면전환이 끝났으면
        {
            if (!techPanel.activeSelf)
            {
                uiCam.SetActive(true);
                techPanel.SetActive(true);
            }
        }
    }
}
