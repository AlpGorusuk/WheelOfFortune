using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Managers;
using WheelOfFortune.UI.Panels;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.UI.Screens
{
    public class HomeScreen : BaseScreen, IObserver
    {
        [SerializeField] private OpenButton openButton;
        [SerializeField] private CollectedItemPanel collectedItemPanel;
        public override void InitScreen()
        {
            base.InitScreen();
            LoadCollectedItemPanel();
        }
        private void Start()
        {
            openButton.Attach(this);
        }
        private void OnDestroy()
        {
            openButton.Detach(this);
        }
        public void UpdateObserver(IObservable observable)
        {
            collectedItemPanel.InitCollectedItemPanel();
            openButton.Show(false);
        }
        private void LoadCollectedItemPanel()
        {
            List<Tuple<int, Sprite>> obtainedItemData = GameManager.Instance.ObtainedItemData;
            collectedItemPanel.LoadObtainedItemPanel(obtainedItemData);
        }
        public void ShowOpenButton() { openButton.Show(true); }
    }
}