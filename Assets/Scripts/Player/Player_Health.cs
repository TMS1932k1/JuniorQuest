using UnityEngine;

public class Player_Health : Entity_Health
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
    }

    public override void ReduceHealth(float damage, out bool isMissed, Transform damageDealer)
    {
        base.ReduceHealth(damage, out isMissed, damageDealer);

        if (!isDead)
            player.stateMachine.ChangeState(player.hurtState);
    }

    public override void Die()
    {
        base.Die();
        player.OnDead();
    }
}

