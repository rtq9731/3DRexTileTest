using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public static GameObject GetTopUI()
    {
        return UIStack.Peek();
    }

    public static bool RemoveUIOnTop()
    {
        if (!IsUIStackEmpty())
        {
            GameObject obj = UIStack.Pop();
            DOTween.Kill(obj);
            obj.transform.DOScale(0, 0.3f).OnComplete(() => obj.SetActive(false));
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void RemoveUIOnTopWithNoTime()
    {
        if (!IsUIStackEmpty())
        {
            GameObject obj = UIStack.Pop();
            DOTween.Kill(obj);
            obj.SetActive(false);
        }
    }

}
