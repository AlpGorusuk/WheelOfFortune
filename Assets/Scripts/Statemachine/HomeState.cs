using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeState : State
{
    private HomeScreen homeScreen;
    public HomeState(UIManager uIManager, HomeScreen homeScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.homeScreen = homeScreen;
    }
    public override void Enter()
    {
        homeScreen.InitHomeScreen();
    }
    public override void Exit()
    {
        base.Exit();
        homeScreen.Hide();
    }
}