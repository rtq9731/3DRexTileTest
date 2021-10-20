using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    [SerializeField] List<LoadingObj> loadings = new List<LoadingObj>();
    [SerializeField] Text textloadProcess;

    int loadingCount = 0;

    public Action finishLoad = () => { };

    public void Start()
    {
        loadings.ForEach(x =>
            {
                x.start += SetText;
                x.finish += SetText;
            });
        SceneManager.LoadSceneAsync("MainScene");
        SceneManager.sceneLoaded += (x, y) => Debug.Log(x.name);
    }

    public void SetNewLoadingObj()
    {

    }

    public void SetText(string text)
    {
        textloadProcess.text = text;
        loadingCount++;
        if(loadingCount >= loadings.Count)
        {
            // ·Îµù ³¡
        }
    }
}
