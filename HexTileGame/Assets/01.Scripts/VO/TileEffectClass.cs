using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffectClass : MonoBehaviour, ITurnFinishObj
{
    int turnforEnd = 0;
    Action turnFinishAct = () => { };
    Action finishEffectAction = () => { };

    public void TurnFinish()
    {
        turnFinishAct();

        turnforEnd--;
        if (turnforEnd <= 0)
        {
            turnFinishAct = () => { };
            finishEffectAction();
            finishEffectAction = () => { }; // ���� �ʱ�ȭ
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
        finishEffectAction += finishEffectAction;
        turnFinishAct += turnFinishAction;
    }
}
