using UnityEngine;

public class Enemy_FreezedState : EnemyState
{
    private SpriteRenderer sr;
    private Color originColor;

    public Enemy_FreezedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        sr = enemy.GetComponentInChildren<SpriteRenderer>();
        originColor = sr.color;
    }

    public override void Enter()
    {
        base.Enter();

        sr.color = Color.blue;
    }

    public override void Exit()
    {
        base.Exit();

        sr.color = originColor;
    }
}
