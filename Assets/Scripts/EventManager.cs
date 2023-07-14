using System;
using System.Collections.Generic;

public class EventManager<T>
{
    private Dictionary<Delegate, Action<T>> eventHandlers;

    public EventManager()
    {
        eventHandlers = new Dictionary<Delegate, Action<T>>();
    }

    public void AddEventListener(Delegate eventName, Action<T> eventHandler)
    {
        if (eventHandlers.ContainsKey(eventName))
        {
            eventHandlers[eventName] += eventHandler;
        }
        else
        {
            eventHandlers[eventName] = eventHandler;
        }
    }

    public void RemoveEventListener(Delegate eventName, Action<T> eventHandler)
    {
        if (eventHandlers.ContainsKey(eventName))
        {
            eventHandlers[eventName] -= eventHandler;
        }
    }

    public void TriggerEvent(Delegate eventName, T args)
    {
        if (eventHandlers.ContainsKey(eventName))
        {
            eventHandlers[eventName]?.Invoke(args);
        }
    }
}
