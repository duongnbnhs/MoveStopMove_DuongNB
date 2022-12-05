using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.Attack();
    }

    public void OnExcute(Bot bot)
    {
        if (!bot.isDead)
        {
            if (!bot.canAttack)
            {
                bot.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
