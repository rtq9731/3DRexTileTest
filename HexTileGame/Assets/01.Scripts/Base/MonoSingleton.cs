using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<GameManager> : MonoBehaviour where GameManager : MonoBehaviour
{
    private static bool isShutDown = false;

    private static object locker = new object();

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {

            if(isShutDown)
            {
                return null;
            }

            lock(locker)
            {
                if (!instance)
                {
                    instance = (GameManager)FindObjectOfType(typeof(GameManager));
                    if (!instance)
                    {
                        GameObject temp = new GameObject(typeof(GameManager).ToString());
                        instance = temp.AddComponent<GameManager>();
                    }

                    DontDestroyOnLoad(instance);
                }
            }

            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        isShutDown = true;
    }
    private void OnDestroy()
    {
        isShutDown = true;
    }
    private void OnEnable()
    {
        isShutDown = false;
    }

}
