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
        if (uiTr != null && whereIsLineEnd != null)
        {
            Vector3 realUIVector = uiTr.position;
            lr.SetPosition(0, realUIVector);

            Vector3 realLineEnd = whereIsLineEnd.position;  
            lr.SetPosition(1, realLineEnd);
        }
    }
}
