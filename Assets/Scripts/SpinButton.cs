using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinButton : BaseButton
{
    public static SpinButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as SpinButton;
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
    public void EnableGameObject(bool _isEnable)
    {
        gameObject.SetActive(_isEnable);
    }
}
