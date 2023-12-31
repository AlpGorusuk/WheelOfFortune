using System;
using UnityEngine;
using WheelOfFortune.Managers;
using WheelOfFortune.UI.Screens;
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
        winScreen.InitScreen(winItem);
    }
    public override void Exit()
    {
        winScreen.Show(false);
    }
    public void SetWinItem(Tuple<Sprite, int, bool> item) { winItem = item; }
}
