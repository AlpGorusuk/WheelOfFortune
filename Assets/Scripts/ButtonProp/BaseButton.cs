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

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Utilities.Utils.StopButtonAnimation(rectTransform);
        gameObject.SetActive(false);
    }
    public void AnimateButton()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Utilities.Utils.AnimateButton(rectTransform, animationDuration, scaleMultiplier, animationEase);
    }
    public void EnableButton(bool isEnable)
    {
        Button _button = GetComponent<Button>();
        _button.enabled = isEnable;
    }
}
