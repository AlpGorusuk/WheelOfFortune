using System;
using System.Collections.Generic;
using UnityEngine;

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
    private void OnDestroy()
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

        CollectButton.Instance.Detach(this);
    }
    public void InitWheelScreen()
    {
        Show();
        WheelManager.Instance.InitWheelManager();
    }
    private void Start()
    {
        obtainedItemPanel = GetComponentInChildren<ObtainedItemPanel>();
        CollectButton.Instance.Attach(this);
    }
    public void UpdateObserver(IObservable observable)
    {
        List<Tuple<int, Sprite>> obtainedItemData = obtainedItemPanel.GetSaveableObtainedItemData();
        GameManager.Instance.SaveGame(obtainedItemData);
        obtainedItemPanel.ClearObtainedItems();
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