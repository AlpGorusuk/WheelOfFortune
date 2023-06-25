
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IInteractable
{
    public Ease animationEase;
    public float animationDuration;
    public void Show() { this.gameObject.SetActive(true); }
    public void Hide() { this.gameObject.SetActive(false); }
    public void InitScreen()
    {
        Show();
        AnimateScreen();
        
    }
    public void AnimateScreen()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Utilities.Utils.AnimateOpenedPanel(rectTransform, rectTransform.localScale, animationDuration, animationEase);
    }
}
