using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OtherCameraUI : MonoBehaviour
{
    [SerializeField] GameObject cvsOtherCamera = null;

    private void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= 0.0005f) // 화면전환이 끝났으면
        {
            if(!gameObject.activeSelf)
            {
                cvsOtherCamera.SetActive(true);
            }
        }
        else
        {
            if (gameObject.activeSelf)
            {
                cvsOtherCamera.SetActive(false);
            }
        }
    }
}
