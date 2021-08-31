using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableUI : MonoBehaviour
{
    private void OnEnable()
    {
        UIStackManager.AddUIToStack(this.gameObject);
    }
}
