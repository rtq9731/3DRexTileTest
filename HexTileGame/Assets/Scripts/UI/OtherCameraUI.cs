using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OtherCameraUI : MonoBehaviour
{
    [SerializeField] GameObject cvsOtherCamera = null;
    [SerializeField] GameObject uiCam = null;

    private void Update()
    {
        if (transform.position == Camera.main.transform.position) // ȭ����ȯ�� ��������
        {
            if (!cvsOtherCamera.activeSelf)
            {
                uiCam.SetActive(true);
                cvsOtherCamera.SetActive(true);
            }
        }
    }
}
