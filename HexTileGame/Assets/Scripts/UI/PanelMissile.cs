using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMissile : MonoBehaviour, ITurnFinishObj
{
    private void OnEnable()
    {
        UIStackManager.RemoveUIOnTopWithNoTime();
        Debug.Log(gameObject);
    }

    public void TurnFinish()
    {
        throw new System.NotImplementedException();
    }
}
