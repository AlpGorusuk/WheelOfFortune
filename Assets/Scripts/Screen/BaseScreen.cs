
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IInteractable
{
    public void Show() { this.gameObject.SetActive(true); }
    public void Hide() { this.gameObject.SetActive(false); }
}