using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private RewardScreen rewardScreen;
    [SerializeField] private WheelScreen wheelScreen;
    [SerializeField] private FailScreen failScreen;
    private WinState winState;
    private FailState failState;
    private PlayState playState;
    private Statemachine statemachine;
    public Action winStateCallback, failStateCallback, playStateCallback;

    public override void Awake()
    {
        base.Awake();
        statemachine = new Statemachine();
        winState = new WinState(this, statemachine);
        failState = new FailState(this, statemachine);
        playState = new PlayState(this, statemachine);

        playStateCallback += PlayStateCallbackFunction;
        winStateCallback += WinStateCallbackFunction;
        failStateCallback += FailStateCallbackFunction;
    }
    private void Start()
    {
        WheelButton.Instance.Attach(this);
    }
    private void OnDisable()
    {
        playStateCallback -= PlayStateCallbackFunction;
        winStateCallback -= WinStateCallbackFunction;
        failStateCallback -= FailStateCallbackFunction;
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
    public void WinStateCallbackFunction()
    {
        rewardScreen.InitRewardScreen();
    }

    public void FailStateCallbackFunction()
    {
        failScreen.InitFailScreen();
    }

    //States-------------------------------
    public void ChangeStateFail()
    {
        statemachine.ChangeState(failState);
    }
    public void ChangeStateWin()
    {
        statemachine.ChangeState(winState);
    }
    //Fail
    // public void 
}