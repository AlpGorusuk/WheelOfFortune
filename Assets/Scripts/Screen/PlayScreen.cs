using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Managers;
using WheelOfFortune.UI.Panels;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.UI.Screens
{
    public class PlayScreen : BaseScreen, IObserver
    {
        public Action<Tuple<Sprite, int, bool>> ItemCollectedCallback;
        public Action ItemCollectFailedCallback;
        private ObtainedItemPanel obtainedItemPanel;
        private void OnEnable()
        {
            WheelManager.Instance.itemObtainedCallback += InitObtainedWheelItemData;
            WheelManager.Instance.SpinStartCallback += () =>
            {
                CollectButton.Instance.EnableButton(false);
                SpinButton.Instance.Hide();
            };
            WheelManager.Instance.SpinStoppedCallback += () =>
            {
                CollectButton.Instance.EnableButton(true);
                SpinButton.Instance.Show();
            };
            ItemCollectedCallback += UIManager.Instance.ChangeStateWin;
            ItemCollectFailedCallback += UIManager.Instance.ChangeStateFail;

        }
        private void OnDisable()
        {
            WheelManager.Instance.itemObtainedCallback -= InitObtainedWheelItemData;
            WheelManager.Instance.SpinStartCallback -= () =>
            {
                CollectButton.Instance.EnableButton(false);
                SpinButton.Instance.Hide();
            };
            WheelManager.Instance.SpinStoppedCallback -= () =>
            {
                CollectButton.Instance.EnableButton(true);
                SpinButton.Instance.Show();
            };
            ItemCollectedCallback -= UIManager.Instance.ChangeStateWin;
            ItemCollectFailedCallback -= UIManager.Instance.ChangeStateFail;
        }
        public new void InitScreen()
        {
            Show();
            AnimateScreen(Vector3.zero, rectTransform.localScale);
            obtainedItemPanel = GetComponentInChildren<ObtainedItemPanel>();
        }
        private void Start()
        {
            CollectButton.Instance.Attach(this);
        }
        private void OnDestroy()
        {
            CollectButton.Instance.Detach(this);
        }
        public void UpdateObserver(IObservable observable)
        {
            List<Tuple<int, Sprite>> targetItemData = obtainedItemPanel.GetSaveableObtainedItemData();
            GameManager.Instance.SaveGame(targetItemData);
            obtainedItemPanel.ClearObtainedItems();
            WheelManager.Instance.ResetCurrentWheel();
            UIManager.Instance.ChangeStateHome();
        }
        private void InitObtainedWheelItemData(Tuple<Sprite, int, bool> obtainedItem)
        {
            bool isFailItem = obtainedItem.Item3;
            if (isFailItem)
            {
                ItemCollectFailedCallback?.Invoke();
            }
            else
            {
                ItemCollectedCallback?.Invoke(obtainedItem);
            }
        }
    }
}