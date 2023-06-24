using UnityEngine;

public class FailScreen : BaseScreen, IObserver
{
    public void InitFailScreen()
    {
        Show();
    }
    private void Start()
    {
        FailButton.Instance.Attach(this);
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
