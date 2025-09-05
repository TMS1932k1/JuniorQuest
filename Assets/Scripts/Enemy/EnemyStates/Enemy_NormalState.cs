using UnityEngine;

public class Enemy_NormalState : EnemyState
{
    public Enemy_NormalState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.DetectPlayer())
        {
            stateMachine.ChangeState(enemy.detectedState);
        }
    }
}
