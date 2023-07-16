using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Managers;
using WheelOfFortune.UI.Panels;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.UI.Screens
{
    public class PlayScreen : BaseScreen, IObserver, IListener
    {
        private ObtainedItemPanel obtainedItemPanel;
        private WheelManager wheelManager;
        public EventManager playScreenEventManager = new EventManager();
        public override void InitScreen()
        {
            base.InitScreen();
            obtainedItemPanel = GetComponentInChildren<ObtainedItemPanel>();
            wheelManager = GetComponentInChildren<WheelManager>();
            //events
            AddEventListener<Tuple<Sprite, int, bool>>(UIManager.Instance.ChangeStateWin);
            AddEventListener<EventArgs>(args => UIManager.Instance.ChangeStateFail());

            wheelManager.AddEventListener<Tuple<Sprite, int, bool>>(InitObtainedWheelItemData);
            wheelManager.AddEventListener<bool>(SpinButton.Instance.Show);
            wheelManager.AddEventListener<bool>(CollectButton.Instance.EnableButton);
        }
        private void Start()
        {
            CollectButton.Instance.Attach(this);
        }
        private void OnDestroy()
        {
            CollectButton.Instance.Detach(this);
            //events
            wheelManager.RemoveEventListener<Tuple<Sprite, int, bool>>(InitObtainedWheelItemData);
            wheelManager.RemoveEventListener<bool>(SpinButton.Instance.Show);
            wheelManager.RemoveEventListener<bool>(CollectButton.Instance.EnableButton);

            RemoveEventListener<Tuple<Sprite, int, bool>>(UIManager.Instance.ChangeStateWin);
            RemoveEventListener<EventArgs>(args => UIManager.Instance.ChangeStateFail());
        }
        public void UpdateObserver(IObservable observable)
        {
            List<Tuple<int, Sprite>> targetItemData = obtainedItemPanel.GetSaveableObtainedItemData();
            GameManager.Instance.SaveGame(targetItemData);
            obtainedItemPanel.ClearObtainedItems();
            wheelManager.ResetCurrentWheel();
            UIManager.Instance.ChangeStateHome();
        }
        private void InitObtainedWheelItemData(Tuple<Sprite, int, bool> obtainedItem)
        {
            bool isFailItem = obtainedItem.Item3;
            if (isFailItem)
            {
                TriggerEvent<EventArgs>(EventArgs.Empty);
            }
            else
            {
                TriggerEvent<Tuple<Sprite, int, bool>>(obtainedItem);
            }
        }
        //Events
        public void AddEventListener<T>(Action<T> eventHandler)
        {
            playScreenEventManager.AddEventListener<T>(eventHandler);
        }

        public void RemoveEventListener<T>(Action<T> eventHandler)
        {
            playScreenEventManager.RemoveEventListener<T>(eventHandler);
        }

        public void TriggerEvent<T>(T eventHandler)
        {
            playScreenEventManager.TriggerEvent<T>(eventHandler);
        }
    }
}