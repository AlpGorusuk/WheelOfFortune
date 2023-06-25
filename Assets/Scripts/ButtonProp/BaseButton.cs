using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour, IObservable, IInteractable
{
    private List<IObserver> observers = new List<IObserver>();
    public virtual void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public virtual void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public virtual void Notify()
    {
        foreach (var observer in observers)
        {
            observer.UpdateObserver(this);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void EnableButton(bool isEnable)
    {
        Button _button = GetComponent<Button>();
        _button.enabled = isEnable;
    }
}
