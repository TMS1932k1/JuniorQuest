using UnityEngine;

public class GolluxSummon_DismissState : EnemyState
{
    private GolluxSummon golluxSummon;


    private Enemy_VFX enemyVFX;
    private Enemy_Health enemyHealth;
    private UI_HealthBar healthBar;
    private ObjectPool<GolluxSummon> objectPool;


    public GolluxSummon_DismissState(string nameState, StateMachine stateMachine, GolluxSummon golluxSummon) : base(nameState, stateMachine, golluxSummon)
    {
        this.golluxSummon = golluxSummon;

        enemyHealth = golluxSummon.GetComponent<Enemy_Health>();
        enemyVFX = golluxSummon.GetComponent<Enemy_VFX>();
        healthBar = golluxSummon.GetComponentInChildren<UI_HealthBar>();

        objectPool = golluxSummon.GetComponentInParent<ObjectPool<GolluxSummon>>();
    }

    public override void Enter()
    {
        base.Enter();

        isTrigger = false;
        ResetVFX();
    }

    public override void Update()
    {
        base.Update();

        if (isTrigger)
        {
            stateMachine.ChangeState(golluxSummon.idleState);
            ResetGolluxSummon();

            objectPool.ReturnObject(golluxSummon);
        }
    }

    private void ResetGolluxSummon()
    {
        enemy.canStunned = true;
        enemyHealth.ResetHealth();
        healthBar.gameObject.SetActive(true);
    }

    private void ResetVFX()
    {
        enemy.canStunned = false;
        enemyVFX.ResetVFX();
    }
}
