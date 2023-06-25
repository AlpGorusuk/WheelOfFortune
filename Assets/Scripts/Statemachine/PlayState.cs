using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    private PlayScreen playScreen;
    public PlayState(UIManager uIManager, PlayScreen wheelScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.playScreen = wheelScreen;
    }
    public override void Enter()
    {
        playScreen.InitScreen();
    }
    public override void Exit()
    {
        playScreen.Hide();
        base.Exit();
    }
}