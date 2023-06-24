namespace Utilities
{
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
    }
}