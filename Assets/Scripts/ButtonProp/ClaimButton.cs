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
    public new void Show()
    {
        gameObject.SetActive(true);
        AnimateButton();
    }
}
