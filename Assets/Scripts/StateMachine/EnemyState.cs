using UnityEngine;

public class EnemyState : EntityState
{
    public Enemy enemy;

    public EnemyState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat(EnemyAnimationStrings.SPEED_MUTIPLIER_PARAM, enemy.GetAnimSpeedMutiplier());
    }
}
