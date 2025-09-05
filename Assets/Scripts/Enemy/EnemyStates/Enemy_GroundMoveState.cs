using UnityEngine;

public class Enemy_GroundMoveState : Enemy_MoveState
{
    public Enemy_GroundMoveState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.faceDir, rb.linearVelocityY);

        if (enemy.wallDetect || !enemy.groundDetect)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
