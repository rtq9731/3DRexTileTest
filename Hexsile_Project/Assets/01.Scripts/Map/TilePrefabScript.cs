using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefabScript : MonoBehaviour
{
    [SerializeField] public TilePrefabType prefabType;
}

public enum TilePrefabType
{
    digSite,
    Plain,
    Ocean,
    Lake
}
