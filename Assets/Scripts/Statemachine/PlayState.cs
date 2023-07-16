using WheelOfFortune.Managers;
using WheelOfFortune.UI.Screens;
public class PlayState : State
{
    private PlayScreen playScreen;
    public PlayState(UIManager uIManager, PlayScreen wheelScreen, Statemachine stateMachine) : base(uIManager, stateMachine)
    {
        this.playScreen = wheelScreen;
    }
    public override void Enter()
    {
        playScreen.InitScreen();
    }
    public override void Exit()
    {
        playScreen.Show(false);
    }
}