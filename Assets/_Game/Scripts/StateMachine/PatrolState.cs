using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    Vector3 target;
    public void OnEnter(Bot bot)
    {
        bot.agent.Resume();
        target = bot.RandomPoint();
        bot.MoveToTarget(target);

        bot.ChangeAnim(StringHelper.ANIM_RUN);
    }

    public void OnExcute(Bot bot)
    {
        if (!bot.isDead)
        {
            if (bot.canAttack || bot.IsTakingTarget())
            {
                bot.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
