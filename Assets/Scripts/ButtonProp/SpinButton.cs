namespace WheelOfFortune.UI.Buttons
{
    public class SpinButton : BaseButton
    {
        public static SpinButton Instance { get; private set; }
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this as SpinButton;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}