using UnityEngine;

public class FailScreen : BaseScreen, IObserver
{
    private void Start()
    {
        Show();
        FailButton.Instance.Attach(this);
    }
    public void UpdateObserver(IObservable observable)
    {
        //Destroy All Collected
        UIManager.Instance.ChangeStatePlay();
    }
}
