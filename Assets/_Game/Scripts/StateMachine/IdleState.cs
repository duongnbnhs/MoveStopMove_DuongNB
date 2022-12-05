using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timeToPatrol;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(StringHelper.ANIM_IDLE);
        bot.isMoving = false;
        bot.agent.Stop();
    }

    public void OnExcute(Bot bot)
    {
        if (!bot.isDead)
        {
            timeToPatrol += Time.deltaTime;
            if (timeToPatrol > 1f)
            {
                timeToPatrol = 0;
                if (bot.canAttack)
                {
                    bot.ChangeState(new AttackState());
                }
                else bot.ChangeState(new PatrolState());
            }
        }
        
    }

    public void OnExit(Bot bot)
    {

    }
}
