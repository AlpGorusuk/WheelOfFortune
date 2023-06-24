using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

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
    //Item sprites
    public SpriteAtlas itemSpriteAtlas;
    public Dictionary<string, Sprite> itemSpriteDictionary = new Dictionary<string, Sprite>();
    public override void Awake()
    {
        base.Awake();
        statemachine = new Statemachine();
        winState = new WinState(this, winScreen, statemachine);
        failState = new FailState(this, failScreen, statemachine);
        playState = new PlayState(this, playScreen, statemachine);
        homeState = new HomeState(this, homeScreen, statemachine);
        //init Sprite
        InitChangeableSpriteAtlas();

    }
    private void Start()
    {
        statemachine.Initialize(homeState);
        WheelButton.Instance.Attach(this);
    }
    private void OnDisable()
    {
        WheelButton.Instance.Detach(this);
    }
    public void UpdateObserver(IObservable observable)
    {
        statemachine.ChangeState(playState);
    }
    //Sprite Atlas-------------------------
    private void InitChangeableSpriteAtlas()
    {
        Sprite[] sprites = new Sprite[itemSpriteAtlas.spriteCount];
        itemSpriteAtlas.GetSprites(sprites);
        foreach (Sprite sprite in sprites)
        {
            itemSpriteDictionary.Add(sprite.name, sprite);
        }
    }
    public Sprite GetSpriteFromAtlas(string spriteName)
    {
        Sprite sprite = null;
        itemSpriteDictionary.TryGetValue(spriteName, out sprite);
        return sprite;
    }
    //States-------------------------------
    public void ChangeStateFail()
    {
        FailCallback?.Invoke();
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
    public void ChangeStateHome()
    {
        statemachine.ChangeState(homeState);
    }
}