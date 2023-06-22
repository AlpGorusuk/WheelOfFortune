using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    public void Attach(IObserver observer);

    public void Detach(IObserver observer);

    public void Notify();
}