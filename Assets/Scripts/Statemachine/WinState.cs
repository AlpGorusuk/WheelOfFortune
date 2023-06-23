using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    private Tuple<Sprite, int, bool> winItem;
    public WinState(UIManager uIManager, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
    }
    public override void Enter()
    {
        uIManager.winStateCallback?.Invoke(winItem);
    }
    public void SetWinItem(Tuple<Sprite, int, bool> item) { winItem = item; }
}
