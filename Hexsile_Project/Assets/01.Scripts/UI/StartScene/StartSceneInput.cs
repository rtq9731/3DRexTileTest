using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneInput : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIStackManager.IsUIStackEmpty())
            {
                UIStackManager.RemoveUIOnTop();
            }
        }
    }
}
