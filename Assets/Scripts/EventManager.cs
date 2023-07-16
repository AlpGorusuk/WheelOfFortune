using System;
using System.Collections.Generic;

public class EventManager
{
    private Dictionary<Type, Delegate> eventHandlers;

    public EventManager()
    {
        eventHandlers = new Dictionary<Type, Delegate>();
    }

    public void AddEventListener<T>(Action<T> eventHandler)
    {
        if (eventHandlers.ContainsKey(typeof(T)))
        {
            eventHandlers[typeof(T)] = (eventHandlers[typeof(T)] as Action<T>) + eventHandler;
        }
        else
        {
            eventHandlers[typeof(T)] = eventHandler;
        }
    }

    public void RemoveEventListener<T>(Action<T> eventHandler)
    {
        if (eventHandlers.ContainsKey(typeof(T)))
        {
            eventHandlers[typeof(T)] = (eventHandlers[typeof(T)] as Action<T>) - eventHandler;
        }
    }

    public void TriggerEvent<T>(T args)
    {
        if (eventHandlers.ContainsKey(typeof(T)))
        {
            (eventHandlers[typeof(T)] as Action<T>)?.Invoke(args);
        }
    }
}