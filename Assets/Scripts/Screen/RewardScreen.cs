using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScreen : MonoBehaviour, IObserver
{
    [SerializeField] private WheelItemContainer obtainedWheelItemContainer;
    public void InitRewardScreen()
    {
        gameObject.SetActive(true);
    }

    public void UpdateObserver(IObservable observable)
    {

    }

    private void Start()
    {
        ClaimButton.Instance.Attach(this);
    }
}
