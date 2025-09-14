using UnityEngine;

public class Enemy_FreezedState : EnemyState
{
    private SpriteRenderer sr;

    public Enemy_FreezedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        sr = enemy.GetComponentInChildren<SpriteRenderer>();
    }

    public override void Enter()
    {
        base.Enter();

        sr.color = Color.blue;
    }

    public override void Exit()
    {
        base.Exit();

        sr.color = Color.white;
    }
}
