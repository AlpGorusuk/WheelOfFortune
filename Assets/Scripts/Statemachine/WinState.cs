using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    private Tuple<Sprite, int, bool> winItem;
    private WinScreen winScreen;
    public WinState(UIManager uIManager, WinScreen rewardScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.winScreen = rewardScreen;
    }
    public override void Enter()
    {
        winScreen.InitWinScreen(winItem);
    }
    public override void Exit()
    {
        base.Exit();
        winScreen.Hide();
    }
    public void SetWinItem(Tuple<Sprite, int, bool> item) { winItem = item; }
}
