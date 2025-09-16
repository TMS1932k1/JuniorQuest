using UnityEngine;

public class Enemy_FreezedState : EnemyState
{
    public Enemy_FreezedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }
}
