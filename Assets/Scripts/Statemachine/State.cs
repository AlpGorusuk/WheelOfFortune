using WheelOfFortune.Managers;
public abstract class State
{
    protected UIManager uIManager;
    protected Statemachine stateMachine;
    protected State(UIManager uIManager, Statemachine stateMachine)
    {
        this.uIManager = uIManager;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Exit();

    public virtual void FixedUpdate()
    {

    }

    public virtual void Update()
    {

    }

}