using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScreen : MonoBehaviour, IObserver
{
    [SerializeField] private WheelManager wheelManager;
    [SerializeField] private CollectedItemPanel collectedItemPanel;
    public void InitWheelScreen()
    {
        wheelManager.InitWheelManager();
        gameObject.SetActive(true);
    }
    public void UpdateObserver(IObservable observable)
    {

    }
}
