using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefabScript : MonoBehaviour
{
    [SerializeField] public TilePrefabType prefabType;
    [SerializeField] public MeshRenderer meshBoder = null;
}

public enum TilePrefabType
{
    digSite,
    Plain,
    Ocean,
    Lake
}
