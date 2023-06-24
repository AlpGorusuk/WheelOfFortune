using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailState : State
{
    private FailScreen failScreen;
    public FailState(UIManager uIManager, FailScreen failScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.failScreen = failScreen;
    }
    public override void Enter()
    {
        //
        failScreen.InitFailScreen();
    }
    public override void Exit()
    {
        failScreen.Hide();
        base.Exit();
    }
}
