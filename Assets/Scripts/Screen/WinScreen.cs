using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : BaseScreen, IObserver
{
    [SerializeField] private WheelItemContainer obtainedWheelItemContainer;
    public void InitWinScreen(Tuple<Sprite, int, bool> rewardItem)
    {
        Show();
        AnimateScreen();
        Sprite _sprite = rewardItem.Item1;
        int _valueText = rewardItem.Item2;
        obtainedWheelItemContainer.UpdateValues(_valueText, _sprite);
    }
    private void Start()
    {
        ClaimButton.Instance.Attach(this);
        ClaimButton.Instance.Show();
    }
    private void OnDestroy()
    {
        ClaimButton.Instance.Detach(this);
    }

    public void UpdateObserver(IObservable observable)
    {
        UIManager.Instance.ChangeStatePlay();
    }
}