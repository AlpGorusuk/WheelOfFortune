using System;
using Unity.VisualScripting;
using UnityEngine;
using WheelOfFortune.Managers;
using WheelOfFortune.UI.Buttons;

namespace WheelOfFortune.UI.Screens
{
    public class WinScreen : BaseScreen, IObserver
    {
        [SerializeField] private WheelItemContainer obtainedWheelItemContainer;
        [SerializeField] private RectTransform effectTransform;
        [SerializeField] private float effectRotataionSpeed;
        public void InitScreen(Tuple<Sprite, int, bool> rewardItem)
        {
            Show(true);
            AnimateScreen(initScale, finalScale);//For Open
            Sprite _sprite = rewardItem.Item1;
            int _valueText = rewardItem.Item2;
            obtainedWheelItemContainer.UpdateValues(_valueText, _sprite);
            PlayEffectAnimation();

            ClaimButton.Instance.Attach(this);
            ClaimButton.Instance.Show(true);
            ClaimButton.Instance.AnimateButton(true);
        }
        private void OnDestroy()
        {
            ClaimButton.Instance.Detach(this);
        }
        private void OnDisable()
        {
            ClaimButton.Instance.AnimateButton(false);
            StopEffectAnimation();
        }
        private void PlayEffectAnimation()
        {
            Utilities.Utils.StartRotateAnimation(effectTransform, effectRotataionSpeed);
        }
        private void StopEffectAnimation()
        {
            Utilities.Utils.StopRotateAnimation(effectTransform);
        }
        public void UpdateObserver(IObservable observable)
        {
            ClaimButton.Instance.Show(false);
            UIManager.Instance.ChangeStatePlay();
        }
    }
}