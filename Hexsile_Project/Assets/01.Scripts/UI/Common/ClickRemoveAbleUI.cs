using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StackableUI))]
public class ClickRemoveAbleUI : MonoBehaviour
{
    [SerializeField] Button btnRemove;
    private void Start()
    {
        btnRemove.onClick.AddListener(() =>
        {
            if(UIStackManager.GetTopUI() == this.gameObject)
            {
                UIStackManager.RemoveUIOnTop();
            }
        });
    }
}
