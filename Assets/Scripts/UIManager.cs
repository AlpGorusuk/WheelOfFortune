using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IObserver
{
    public WinScreen winScreen;
    public PlayScreen playScreen;
    public FailScreen failScreen;
    public HomeScreen homeScreen;
    public WinState winState;
    public FailState failState;
    public PlayState playState;
    public HomeState homeState;
    public Statemachine statemachine;
    public Action FailCallback;
    public Action<Tuple<Sprite, int, bool>> WinCallback;
    public override void Awake()
    {
        base.Awake();
        statemachine = new Statemachine();
        winState = new WinState(this, winScreen, statemachine);
        failState = new FailState(this, failScreen, statemachine);
        playState = new PlayState(this, playScreen, statemachine);
        homeState = new HomeState(this, homeScreen, statemachine);

    }
    private void Start()
    {
        statemachine.Initialize(homeState);
        WheelButton.Instance.Attach(this);
        //
        FailCallback += () => statemachine.ChangeState(failState);
        WinCallback += ChangeStateWin;

    }
    private void OnDisable()
    {
        WheelButton.Instance.Detach(this);
        FailCallback -= () => statemachine.ChangeState(failState);
        WinCallback -= ChangeStateWin;
    }
    public void UpdateObserver(IObservable observable)
    {
        statemachine.ChangeState(playState);
    }

    //States-------------------------------
    public void ChangeStateFail()
    {
        FailCallback?.Invoke();
    }
    public void ChangeStateWin(Tuple<Sprite, int, bool> wheelItem)
    {
        WinCallback?.Invoke(wheelItem);
    }
    public void ChangeStatePlay()
    {
        statemachine.ChangeState(playState);
    }
}