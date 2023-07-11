using System;
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
        public void InitWinScreen(Tuple<Sprite, int, bool> rewardItem)
        {
            Show();
            AnimateScreen(Vector3.zero, rectTransform.localScale);
            Sprite _sprite = rewardItem.Item1;
            int _valueText = rewardItem.Item2;
            obtainedWheelItemContainer.UpdateValues(_valueText, _sprite);
            ClaimButton.Instance.Show();
            PlayEffectAnimation();
        }
        private void Start()
        {
            ClaimButton.Instance.Attach(this);
        }
        private void PlayEffectAnimation()
        {
            Utilities.Utils.StartRotateAnimation(effectTransform, effectRotataionSpeed);
        }
        private void StopEffectAnimation()
        {
            Utilities.Utils.StopRotateAnimation(effectTransform);
        }
        private void OnDestroy()
        {
            ClaimButton.Instance.Detach(this);
        }

        public void UpdateObserver(IObservable observable)
        {
            ClaimButton.Instance.Hide();
            StopEffectAnimation();
            UIManager.Instance.ChangeStatePlay();
        }
    }
}