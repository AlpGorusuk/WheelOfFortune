using WheelOfFortune.Managers;
using WheelOfFortune.UI.Screens;
public class HomeState : State
{
    private HomeScreen homeScreen;
    public HomeState(UIManager uIManager, HomeScreen homeScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.homeScreen = homeScreen;
    }
    public override void Enter()
    {
        homeScreen.InitHomeScreen();
    }
    public override void Exit()
    {
        homeScreen.Hide();
    }
}