using UnityEngine;

public class Enemy_MoveState : Enemy_NormalState
{
    public Enemy_MoveState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }
}
