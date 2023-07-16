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

            FailButton.Instance.Attach(this);
            FailButton.Instance.Show(true);
            FailButton.Instance.AnimateButton(true);
        }

        private void OnDisable()
        {
            FailButton.Instance.AnimateButton(false);
        }
        private void OnDestroy()
        {
            FailButton.Instance.Detach(this);
        }
        public void UpdateObserver(IObservable observable)
        {
            FailButton.Instance.Show(false);
            UIManager.Instance.ChangeStatePlay();
        }
    }
}