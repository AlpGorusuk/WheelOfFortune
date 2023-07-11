using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using WheelOfFortune.UI.Screens;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.Managers
{
    public class UIManager : Singleton<UIManager>, IObserver
    {
        public WinScreen winScreen;
        public PlayScreen playScreen;
        public FailScreen failScreen;
        public HomeScreen homeScreen;
        private WinState winState;
        private FailState failState;
        private PlayState playState;
        private HomeState homeState;
        private Statemachine statemachine;
        public Action FailCallback;
        //Item sprites
        [SerializeField] private SpriteAtlas itemSpriteAtlas;
        public SpriteAtlas ItemSpriteAtlas { get => itemSpriteAtlas; }
        public Dictionary<string, Sprite> itemSpriteDictionary = new Dictionary<string, Sprite>();
        private void Start()
        {
            SetStates();
            statemachine.Initialize(homeState);
            //
            WheelButton.Instance.Attach(this);
            //
            InitSpriteAtlasDictionary();
        }

        private void SetStates()
        {
            statemachine = new Statemachine();
            //
            winState = new WinState(this, winScreen, statemachine);
            failState = new FailState(this, failScreen, statemachine);
            playState = new PlayState(this, playScreen, statemachine);
            homeState = new HomeState(this, homeScreen, statemachine);
        }

        private void OnDestroy()
        {
            WheelButton.Instance.Detach(this);
        }
        public void UpdateObserver(IObservable observable)
        {
            statemachine.ChangeState(playState);
        }
        //Sprite Atlas-------------------------
        public void InitSpriteAtlasDictionary()
        {
            Sprite[] sprites = new Sprite[ItemSpriteAtlas.spriteCount];
            ItemSpriteAtlas.GetSprites(sprites);
            foreach (Sprite sprite in sprites)
            {
                itemSpriteDictionary.Add(sprite.name, sprite);
            }
        }
        public Sprite GetSpriteFromDictionary(string spriteName)
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
}