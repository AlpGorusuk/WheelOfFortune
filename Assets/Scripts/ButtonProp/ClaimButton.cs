
namespace WheelOfFortune.UI.Buttons
{
    public class ClaimButton : BaseButton
    {
        public static ClaimButton Instance { get; private set; }
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this as ClaimButton;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}