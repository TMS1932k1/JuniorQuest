using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;

    public Transform damageTransform { get; private set; }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public override void ReduceHealth(float damage, Transform damageTransform)
    {
        base.ReduceHealth(damage, damageTransform);

        if (enemy.GetCurrentState() != enemy.attackState && enemy.GetCurrentState() != enemy.playerDetectedState)
        {
            this.damageTransform = damageTransform;
            enemy.stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
}
