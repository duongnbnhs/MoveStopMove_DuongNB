using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IState
{
    //vao state
    public void OnEnter(BotAI bot);
    //update state
    public void OnExcute(BotAI bot);
    //thoat khoi state
    public void OnExit(BotAI bot);
}
