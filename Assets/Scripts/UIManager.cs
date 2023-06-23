using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private RewardScreen rewardScreen;
    [SerializeField] private WheelScreen wheelScreen;
    [SerializeField] private FailScreen failScreen;
    [SerializeField] private HomeScreen homeScreen;
    private WinState winState;
    private FailState failState;
    private PlayState playState;
    private HomeState homeState;
    private Statemachine statemachine;
    public Action failStateCallback, playStateCallback;
    public Action<Tuple<Sprite, int, bool>> winStateCallback;
    public override void Awake()
    {
        base.Awake();
        statemachine = new Statemachine();
        winState = new WinState(this, statemachine);
        failState = new FailState(this, statemachine);
        playState = new PlayState(this, statemachine);
        homeState = new HomeState(this, statemachine);

        playStateCallback += PlayStateCallbackFunction;
        winStateCallback += WinStateCallbackFunction;
        failStateCallback += FailStateCallbackFunction;
    }
    private void Start()
    {
        WheelButton.Instance.Attach(this);
        statemachine.Initialize(homeState);
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
        statemachine.ChangeState(playState);
    }
    private void PlayStateCallbackFunction()
    {
        wheelScreen.InitWheelScreen();
    }
    public void WinStateCallbackFunction(Tuple<Sprite, int, bool> winItem)
    {
        rewardScreen.InitRewardScreen(winItem);
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
    public void ChangeStateWin(Tuple<Sprite, int, bool> wheelItem)
    {
        winState.SetWinItem(wheelItem);
        statemachine.ChangeState(winState);
    }
    public void ChangeStatePlay()
    {
        statemachine.ChangeState(playState);
    }
}