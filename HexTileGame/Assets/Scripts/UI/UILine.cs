using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILine : MonoBehaviour
{
    [SerializeField] Transform uiTr = null;
    [SerializeField] Transform whereIsLineEnd = null;

    private LineRenderer lr = null;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(lr.isVisible && uiTr != null && whereIsLineEnd != null)
        {
            Vector3 realUIVector = Camera.main.ScreenToWorldPoint(uiTr.position);
            realUIVector.z = -1;
            lr.SetPosition(0, realUIVector);

            Vector3 realLineEnd = Camera.main.ScreenToWorldPoint(whereIsLineEnd.position);  
            realLineEnd.z = -1;
            lr.SetPosition(1, realLineEnd);
        }
    }
}
