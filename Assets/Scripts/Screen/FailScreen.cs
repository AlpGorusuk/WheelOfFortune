using UnityEngine;

public class FailScreen : MonoBehaviour, IObserver
{
    public void InitFailScreen()
    {
        gameObject.SetActive(true);
    }

    public void UpdateObserver(IObservable observable)
    {
        //Destroy All Collected
    }

    private void Start()
    {
        FailButton.Instance.Attach(this);
    }
}
