using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T entityMain;
    private IState<T> currentState;

    public void Initialize(T mainEntity, IState<T> changeState)
    {
        entityMain = mainEntity;
        currentState = null;
        StateChage(changeState);
    }

    public void StateChage(IState<T> changeState)
    {
        if (changeState == null) { return; }

        if (currentState != null) { currentState.StateExit(entityMain); }

        currentState = changeState;
        currentState.StateEnter(entityMain);
    }

    public void OnStateUpdate() { if (currentState != null) { currentState.StateUpdate(entityMain); } }
}
