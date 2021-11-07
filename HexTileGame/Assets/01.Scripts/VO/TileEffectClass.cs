using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileEffectClass : MonoBehaviour, ITurnFinishObj
{
    [SerializeField]
    int turnforEnd = 0;
    Action turnFinishAct = () => { };
    Action finishEffectAct = () => { };

    public void TurnFinish()
    {
        turnFinishAct();

        turnforEnd--;
        if (turnforEnd <= 0)
        {
            turnFinishAct = () => { };
            finishEffectAct();
            finishEffectAct = () => { }; // ���� �ʱ�ȭ
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="turnFinishAction">���� ���������� ���� �ൿ�Դϴ�</param>
    /// <param name="turn">���ϵ��� ���ӵ��� �����ϴ� �����Դϴ�.</param>
    /// <param name="finishEffectAction">ȿ���� ������ ��, �������� �ൿ�Դϴ�.</param>
    public TileEffectClass(Action turnFinishAction, int turn, Action finishEffectAction)
    {
        turnforEnd = 0;
        finishEffectAct += finishEffectAction;
        turnFinishAct += turnFinishAction;
    }
}
