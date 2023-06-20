using System;
using UnityEngine;

public class SpinButton : Observable
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
}
