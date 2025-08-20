using UnityEngine;

public class Player_Health : Entity_Health
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
    }

    public override void ReduceHealth(float damage, Transform damageDealer)
    {
        base.ReduceHealth(damage, damageDealer);

        if (!isDead)
            player.stateMachine.ChangeState(player.hurtState);
    }

    protected override void Die()
    {
        base.Die();
        player.OnDead();
    }
}

