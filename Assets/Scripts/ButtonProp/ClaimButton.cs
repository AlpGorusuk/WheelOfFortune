using System;
using System.Collections.Generic;
using UnityEngine;

public class ClaimButton : BaseButton
{
    public static ClaimButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as ClaimButton;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void Attach(IObserver observer)
    {
        base.Attach(observer);
    }
    public override void Detach(IObserver observer)
    {
        base.Detach(observer);
    }
    public override void Notify()
    {
        base.Notify();
    }
}
