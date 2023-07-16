using UnityEngine;
using WheelOfFortune.Managers;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.UI.Screens
{
    public class FailScreen : BaseScreen, IObserver
    {
        public override void InitScreen()
        {
            base.InitScreen();
            FailButton.Instance.Show(true);
            FailButton.Instance.AnimateButton(true);
        }
        private void Start()
        {
            FailButton.Instance.Attach(this);
        }
        private void OnDestroy()
        {
            FailButton.Instance.Detach(this);
            FailButton.Instance.AnimateButton(false);
        }
        public void UpdateObserver(IObservable observable)
        {
            FailButton.Instance.Show(false);
            UIManager.Instance.ChangeStatePlay();
        }
    }
}