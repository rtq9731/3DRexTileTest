using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScript : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0f;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material.mainTextureOffset = Vector2.zero;
    }

    private void Update()
    {

        sr.material.mainTextureOffset = new Vector2(sr.material.mainTextureOffset.x - Time.deltaTime * scrollSpeed, sr.material.mainTextureOffset.y);
    }
}
