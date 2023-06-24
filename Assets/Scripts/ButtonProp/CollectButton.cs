using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectButton : BaseButton
{
    public static CollectButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as CollectButton;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
