using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    public PlayState(UIManager uIManager, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
    }
    public override void Enter()
    {
        uIManager.playStateCallback?.Invoke();
    }
}