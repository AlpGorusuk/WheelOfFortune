using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private RewardScreen rewardScreen;
    [SerializeField] private WheelScreen wheelScreen;
    private WinState winState;
    private FailState failState;
    private PlayState playState;
    private Statemachine statemachine;
    public Action winStateCallback, failStateCallback, playStateCallback;

    public override void Awake()
    {
        statemachine = new Statemachine();
        winState = new WinState(this, statemachine);
        failState = new FailState(this, statemachine);
        playState = new PlayState(this, statemachine);

        playStateCallback += PlayStateCallbackFunction;
    }
    private void Start()
    {
        WheelButton.Instance.Attach(this);
    }
    private void OnDisable()
    {
        playStateCallback -= PlayStateCallbackFunction;
        WheelButton.Instance.Detach(this);
    }
    public void UpdateObserver(IObservable observable)
    {
        statemachine.Initialize(playState);
    }
    private void PlayStateCallbackFunction()
    {
        wheelScreen.InitWheelScreen();
    }
}