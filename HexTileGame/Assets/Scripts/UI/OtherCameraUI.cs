using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OtherCameraUI : MonoBehaviour
{
    [SerializeField] GameObject cvsOtherCamera = null;

    private void Update()
    {
        if (transform.position == Camera.main.transform.position) // 화면전환이 끝났으면
        {
            if (!cvsOtherCamera.activeSelf)
            {
                cvsOtherCamera.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        cvsOtherCamera.SetActive(false);
    }
}
