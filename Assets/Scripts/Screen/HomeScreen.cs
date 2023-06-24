using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : BaseScreen, IObserver
{
    public void InitHomeScreen()
    {
        Show();
    }

    public void UpdateObserver(IObservable observable)
    {

    }
}