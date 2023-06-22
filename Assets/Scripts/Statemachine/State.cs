using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected UIManager uIManager;
    protected Statemachine stateMachine;
    protected State(UIManager uIManager, Statemachine stateMachine)
    {
        this.uIManager = uIManager;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }
}