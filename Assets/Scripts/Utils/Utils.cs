namespace Utilities
{
    using DG.Tweening;
    using UnityEngine;

    public static class Utils
    {
        public static string FormatNumber(int number)
        {
            const int Billion = 1000000000;
            const int Million = 1000000;
            const int Thousand = 1000;

            if (number >= Billion)
            {
                return (number / Billion).ToString("0.#") + "B";
            }
            if (number >= Million)
            {
                return (number / Million).ToString("0.#") + "M";
            }
            if (number >= Thousand)
            {
                return (number / Thousand).ToString("0.#") + "K";
            }

            return number.ToString();
        }
        public static string RemoveUnwantedText(string originalString, string unwantedText)
        {
            int index = originalString.IndexOf(unwantedText);
            if (index >= 0)
            {
                return originalString.Substring(0, index) + originalString.Substring(index + unwantedText.Length);
            }
            else
            {
                return originalString;
            }
        }
        public static void AnimatePanel(RectTransform panelTransform, Vector3 setScale, Vector3 targetScale, float animationDuration, Ease animationEase)
        {
            panelTransform.localScale = setScale;

            panelTransform.DOScale(targetScale, animationDuration)
                .SetEase(animationEase);
        }
        static Sequence scaleSequence, rotateSequence;
        //Scale anim
        public static void AnimateButton(RectTransform rectTransform, float animationDuration, float scaleMultiplier, Ease animationEase)
        {
            Vector3 originalScale = rectTransform.localScale;

            scaleSequence = DOTween.Sequence()
                .Append(rectTransform.DOScale(originalScale * scaleMultiplier, animationDuration).SetEase(animationEase))
                .Append(rectTransform.DOScale(originalScale, animationDuration).SetEase(animationEase))
                .SetLoops(-1);

            scaleSequence.Play();
        }
        public static void StopButtonAnimation(RectTransform rectTransform)
        {
            scaleSequence.Kill();
            rectTransform.localScale = Vector3.one;
        }
        //Rotate Anim
        public static void StartRotateAnimation(RectTransform targetTransform, float rotationSpeed)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(targetTransform.DORotate(new Vector3(0f, 0f, 360f), rotationSpeed, RotateMode.FastBeyond360))
                .SetLoops(-1, LoopType.Restart);
            sequence.Play();
        }
        public static void StopRotateAnimation(RectTransform rectTransform)
        {
            rotateSequence.Kill();
            rectTransform.localScale = Vector3.one;
        }
    }
}