using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartColorData : MonoBehaviour
{
    [SerializeField] public Color[] colorSet;

    void Start()
    {
        GameManager.Instance.colorSet = colorSet;
    }
}
