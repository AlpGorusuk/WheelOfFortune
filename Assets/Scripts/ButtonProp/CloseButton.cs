using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : BaseButton
{
    public static CloseButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as CloseButton;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
