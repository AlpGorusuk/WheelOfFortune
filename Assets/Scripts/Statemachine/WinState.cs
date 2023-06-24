using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    private Tuple<Sprite, int, bool> winItem;
    private WinScreen rewardScreen;
    public WinState(UIManager uIManager, WinScreen rewardScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.rewardScreen = rewardScreen;
    }
    public override void Enter()
    {
        rewardScreen.InitWinScreen(winItem);
    }
    public override void Exit()
    {
        base.Exit();
        rewardScreen.Hide();
    }
    public void SetWinItem(Tuple<Sprite, int, bool> item) { winItem = item; }
}
