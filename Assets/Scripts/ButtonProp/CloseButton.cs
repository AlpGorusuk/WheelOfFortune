namespace WheelOfFortune.UI.Buttons
{
    public class CloseButton : BaseButton
    {
        public static CloseButton Instance { get; private set; }
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this as CloseButton;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}