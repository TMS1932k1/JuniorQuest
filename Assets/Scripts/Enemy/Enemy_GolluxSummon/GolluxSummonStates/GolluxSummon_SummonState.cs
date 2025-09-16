using UnityEngine;

public class GolluxSummon_SummonState : EnemyState
{
    public GolluxSummon_SummonState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isTrigger = false;
    }

    public override void Update()
    {
        base.Update();

        if (isTrigger)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
