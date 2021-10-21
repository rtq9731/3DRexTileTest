using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoadingObj : MonoBehaviour
{
    LoadManager loadManager = null;

    public Action<string> start;
    public Action<string> finish;
}
