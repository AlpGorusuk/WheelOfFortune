using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : BaseScreen, IObserver
{
    [SerializeField] private WheelItemContainer obtainedWheelItemContainer;
    public void InitRewardScreen(Tuple<Sprite, int, bool> rewardItem)
    {
        Show();
        ClaimButton.Instance.Attach(this);
        Sprite _sprite = rewardItem.Item1;
        int _valueText = rewardItem.Item2;
        obtainedWheelItemContainer.UpdateValues(_valueText, _sprite);
    }

    public void UpdateObserver(IObservable observable)
    {
        UIManager.Instance.ChangeStatePlay();
    }
}