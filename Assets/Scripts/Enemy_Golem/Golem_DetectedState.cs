using UnityEngine;

public class Golem_DetectedState : Enemy_DetectedState
{
    public Golem_DetectedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isAttack)
        {
            //Change attack state
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            // Move dir to player
            enemy.SetVelocity(enemy.moveDetectedSpeed * GetDirectToPlayer(), rb.linearVelocityY);
        }
    }
}
