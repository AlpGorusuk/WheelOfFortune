using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour, IObserver
{
    public void InitRewardScreen()
    {
        gameObject.SetActive(true);
    }

    public void UpdateObserver(IObservable observable)
    {

    }

    private void Start()
    {

    }
}