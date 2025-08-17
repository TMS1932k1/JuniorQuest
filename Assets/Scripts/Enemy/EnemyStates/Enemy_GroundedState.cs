using UnityEngine;

public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.DetectPlayer())
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
}
