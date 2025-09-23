using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    protected Enemy_VFX enemyVFX;
    protected Entity_SFX entitySFX;


    public EnemyState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        this.enemy = enemy;
        enemyVFX = enemy.GetComponent<Enemy_VFX>();
        entitySFX = enemy.GetComponent<Entity_SFX>();

        rb = enemy.rb;
        anim = enemy.anim;
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat(EnemyAnimationStrings.SPEED_MUTIPLIER_PARAM, enemy.GetAnimSpeedMutiplier());
    }
}
