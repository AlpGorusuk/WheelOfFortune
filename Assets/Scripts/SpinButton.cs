using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinButton : MonoBehaviour, IObservable
{
    public static SpinButton Instance { get; private set; }
    private List<IObserver> observers = new List<IObserver>();

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

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.UpdateObserver(this);
        }
    }
}
