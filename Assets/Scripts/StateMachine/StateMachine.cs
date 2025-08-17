using UnityEngine;

public class StateMachine
{
    public EntityState currentState;

    public void Initialize(EntityState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(EntityState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void CurrentStateUpdate()
    {
        currentState.Update();
    }
}
