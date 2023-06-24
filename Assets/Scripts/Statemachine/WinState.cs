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
        uIManager.WinCallback += SetWinItem;
        rewardScreen.InitRewardScreen(winItem);
    }
    public override void Exit()
    {
        uIManager.WinCallback -= SetWinItem;
        base.Exit();
        rewardScreen.Hide();
    }
    private void SetWinItem(Tuple<Sprite, int, bool> item) { winItem = item; }
}
