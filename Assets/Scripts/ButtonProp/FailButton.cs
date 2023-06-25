using System;
using System.Collections.Generic;
using UnityEngine;

public class FailButton : BaseButton
{
    public static FailButton Instance { get; private set; }
    public new void Show()
    {
        gameObject.SetActive(true);
        AnimateButton();
    }
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
