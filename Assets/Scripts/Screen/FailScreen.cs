using UnityEngine;

public class FailScreen : BaseScreen, IObserver
{
    private void Start()
    {
        FailButton.Instance.Attach(this);
        FailButton.Instance.Show();
    }
    private void OnDestroy()
    {
        FailButton.Instance.Detach(this);
    }
    public void UpdateObserver(IObservable observable)
    {
        //Destroy All Collected
        UIManager.Instance.ChangeStatePlay();
    }
}
