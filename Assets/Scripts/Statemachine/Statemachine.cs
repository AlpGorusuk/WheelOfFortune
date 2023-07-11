public class Statemachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
    public void Update_Statemachine()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
    public void FixedUpdate_Statemachine()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }
}
