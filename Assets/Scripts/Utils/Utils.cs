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
        public static void AnimateOpenedPanel(RectTransform panelTransform, Vector3 originalScale, float animationDuration, Ease animationEase)
        {
            panelTransform.localScale = Vector3.zero;

            panelTransform.DOScale(originalScale, animationDuration)
                .SetEase(animationEase);
        }
        public static void AnimateButton(RectTransform rectTransform, float animationDuration, float scaleMultiplier, Ease animationEase)
        {
            Vector3 originalScale = rectTransform.localScale;

            Sequence sequence = DOTween.Sequence()
                .Append(rectTransform.DOScale(originalScale * scaleMultiplier, animationDuration).SetEase(animationEase))
                .Append(rectTransform.DOScale(originalScale, animationDuration).SetEase(animationEase))
                .SetLoops(-1);

            sequence.Play();
        }
    }
}