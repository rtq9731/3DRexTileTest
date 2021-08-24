using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] LayerMask whatIsTile;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Camera.main.farClipPlane, whatIsTile))
            {
                Debug.Log(hit.transform.GetComponent<TileScript>().Data.type);
            }

        }
    }

}
