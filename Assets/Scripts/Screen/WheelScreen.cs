using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScreen : MonoBehaviour
{
    [SerializeField] private WheelManager wheelManager;
    public void InitWheelScreen()
    {
        wheelManager.InitWheelManager();
        gameObject.SetActive(true);
    }
}
