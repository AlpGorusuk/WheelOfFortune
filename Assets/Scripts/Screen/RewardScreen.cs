using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScreen : MonoBehaviour, IObserver
{
    [SerializeField] private WheelItemContainer obtainedWheelItemContainer;
    public void InitRewardScreen(Tuple<Sprite, int, bool> rewardItem)
    {
        gameObject.SetActive(true);
        obtainedWheelItemContainer.ImageSprite = rewardItem.Item1;
        obtainedWheelItemContainer.ValueText = rewardItem.Item2.ToString();
        obtainedWheelItemContainer.UpdateValues();
    }

    public void UpdateObserver(IObservable observable)
    {

    }

    private void Start()
    {

    }
}
