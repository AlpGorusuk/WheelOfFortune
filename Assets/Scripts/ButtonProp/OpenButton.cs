using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : BaseButton
{
    public static OpenButton Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this as OpenButton;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
