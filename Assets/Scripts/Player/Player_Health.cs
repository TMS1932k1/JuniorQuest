using UnityEngine;

public class Player_Health : Entity_Health, ISaveable
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

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save Health of Player");

        gameData.playerHealth = currentHealth;
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load Health of Player");

        currentHealth = gameData.playerHealth;

        if (currentHealth > 0)
            isDead = false;
    }
}

