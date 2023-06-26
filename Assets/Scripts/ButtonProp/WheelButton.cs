using System;
using System.Collections.Generic;
using UnityEngine;

public class WheelButton : BaseButton
{
    public static WheelButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as WheelButton;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}