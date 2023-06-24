using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayScreen : BaseScreen, IObserver
{
    public Action<Tuple<Sprite, int, bool>> ItemCollectedCallback;
    private void OnEnable()
    {
        WheelManager.Instance.wheelSpinnedCallback += InitObtainedWheelItemData;
    }
    private void OnDisable()
    {
        WheelManager.Instance.wheelSpinnedCallback -= InitObtainedWheelItemData;
    }
    public void InitWheelScreen()
    {
        Show();
        WheelManager.Instance.InitWheelManager();
        CollectButton.Instance.Attach(this);
    }
    public void UpdateObserver(IObservable observable)
    {

    }
    private void InitObtainedWheelItemData(Tuple<Sprite, int, bool> obtainedItem)
    {
        bool isFailItem = obtainedItem.Item3;
        if (isFailItem)
        {
            UIManager.Instance.ChangeStateFail();
        }
        else
        {
            UIManager.Instance.ChangeStateWin(obtainedItem);
            ItemCollectedCallback?.Invoke(obtainedItem);
        }
    }
}