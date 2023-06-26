
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IInteractable
{
    public Ease animationEase;
    public float animationDuration;
    protected RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Show() { this.gameObject.SetActive(true); }
    public void Hide() { this.gameObject.SetActive(false); }
    public void InitScreen()
    {
        Show();
        AnimateScreen(Vector3.zero, rectTransform.localScale);//For Open

    }
    public void AnimateScreen(Vector3 setScale, Vector3 targetScale)
    {
        Utilities.Utils.AnimatePanel(rectTransform, setScale, targetScale, animationDuration, animationEase);
    }
}
