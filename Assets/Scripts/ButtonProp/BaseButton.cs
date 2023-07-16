using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public abstract class BaseButton : MonoBehaviour, IObservable, IInteractable
{
    public Ease animationEase;
    public float animationDuration, scaleMultiplier;
    private List<IObserver> observers = new List<IObserver>();
    public virtual void Attach(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            return;
        }
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
    public void AnimateButton(bool isEnable)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (isEnable)
        {
            Utilities.Utils.AnimateButton(rectTransform, animationDuration, scaleMultiplier, animationEase);
        }
        else
        {
            Utilities.Utils.StopButtonAnimation(rectTransform);
        }
    }
    public void EnableButton(bool isEnable)
    {
        Button _button = GetComponent<Button>();
        _button.enabled = isEnable;
    }

    public void Show(bool _value)
    {
        if (_value == true)
        {
            gameObject.SetActive(_value);
        }
        else
        {
            gameObject.SetActive(_value);
        }
    }
}
