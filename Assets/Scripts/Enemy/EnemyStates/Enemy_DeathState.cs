using UnityEngine;

public class Enemy_DeathState : EnemyState
{
    private Collider2D col;

    public Enemy_DeathState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        col = enemy.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(5 * -enemy.faceDir, 15);
        rb.gravityScale = 10;
        col.enabled = false;
    }
}
