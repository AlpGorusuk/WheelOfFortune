using System;
using System.Collections.Generic;
using UnityEngine;

public class FailButton : BaseButton
{
    public static FailButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as FailButton;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
