using DG.Tweening;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IInteractable
{
    public Ease animationEase;
    public float animationDuration;
    protected RectTransform rectTransform;
    protected Vector3 initScale = Vector3.zero, finalScale = Vector3.one;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public virtual void InitScreen()
    {
        Show(true);
        AnimateScreen(initScale, finalScale);//For Open

    }
    public void AnimateScreen(Vector3 setScale, Vector3 targetScale)
    {
        Utilities.Utils.AnimatePanel(rectTransform, setScale, targetScale, animationDuration, animationEase);
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