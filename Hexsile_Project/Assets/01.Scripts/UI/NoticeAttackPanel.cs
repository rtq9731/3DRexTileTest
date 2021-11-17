using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeAttackPanel : MonoBehaviour
{
    [SerializeField] Text noticeText = null;
    public void Refresh(string str)
    {
        noticeText.text = str;
    }
}
