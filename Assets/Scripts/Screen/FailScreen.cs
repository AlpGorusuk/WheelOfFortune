using UnityEngine;

public class FailScreen : BaseScreen, IObserver
{
    public new void InitScreen()
    {
        Show();
        AnimateScreen(Vector3.zero, rectTransform.localScale);
        FailButton.Instance.Show();
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
        FailButton.Instance.Hide();
        UIManager.Instance.ChangeStatePlay();
    }
}
