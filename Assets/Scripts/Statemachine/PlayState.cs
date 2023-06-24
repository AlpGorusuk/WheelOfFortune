using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    private PlayScreen wheelScreen;
    public PlayState(UIManager uIManager, PlayScreen wheelScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.wheelScreen = wheelScreen;
    }
    public override void Enter()
    {
        wheelScreen.InitWheelScreen();
    }
}