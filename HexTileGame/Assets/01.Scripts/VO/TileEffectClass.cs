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
            finishEffectAct = () => { }; // 전부 초기화
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="turnFinishAction">턴이 끝날때마다 해줄 행동입니다</param>
    /// <param name="turn">몇턴동안 지속될지 결정하는 변수입니다.</param>
    /// <param name="finishEffectAction">효과가 끝났을 때, 실행해줄 행동입니다.</param>
    public TileEffectClass(Action turnFinishAction, int turn, Action finishEffectAction)
    {
        turnforEnd = 0;
        finishEffectAct += finishEffectAction;
        turnFinishAct += turnFinishAction;
    }
}
