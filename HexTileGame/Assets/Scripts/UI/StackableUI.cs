using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableUI : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector2.one;
        UIStackManager.AddUIToStack(this.gameObject);
    }
}
