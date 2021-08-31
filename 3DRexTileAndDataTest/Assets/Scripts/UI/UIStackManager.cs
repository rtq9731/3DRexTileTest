using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStackManager
{
    static Stack<GameObject> UIStack = new Stack<GameObject>();

    public static void AddUIToStack(GameObject item)
    {
        UIStack.Push(item);
    }

    public static bool IsUIStackEmpty()
    {
        return UIStack.Count <= 0 ? true : false;
    }

    public static bool RemoveUIOnTop(out GameObject topUI)
    {
        topUI = UIStack.Pop();

        if(topUI != null)
        {
            topUI.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool RemoveUIOnTop()
    {
        if (!IsUIStackEmpty())
        {
            UIStack.Pop().SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

}
