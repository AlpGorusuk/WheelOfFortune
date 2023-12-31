using WheelOfFortune.Managers;
using WheelOfFortune.UI.Screens;
public class FailState : State
{
    private FailScreen failScreen;
    public FailState(UIManager uIManager, FailScreen failScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.failScreen = failScreen;
    }
    public override void Enter()
    {
        //
        failScreen.InitScreen();
    }
    public override void Exit()
    {
        failScreen.Show(false);
    }
}
