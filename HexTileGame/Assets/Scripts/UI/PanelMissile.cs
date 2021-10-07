using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMissile : MonoBehaviour
{
    private void OnEnable()
    {
        UIStackManager.RemoveUIOnTopWithNoTime();
        Debug.Log(gameObject);
    }

}
