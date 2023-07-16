using System;
using System.Collections.Generic;
using UnityEngine;
public interface IListener
{
    public void AddEventListener<T>(Action<T> eventHandler);
    public void RemoveEventListener<T>(Action<T> eventHandler);
    public void TriggerEvent<T>(T eventHandler);
}