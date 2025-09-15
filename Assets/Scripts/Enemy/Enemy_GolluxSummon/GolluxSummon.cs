using UnityEngine;

public class GolluxSummon : Golem
{
    public GolluxSummon_DismissState dismissState;
    public bool isDismiss { get; private set; }


    private Enemy_Health enemyHealth;


    protected override void Awake()
    {
        base.Awake();

        enemyHealth = GetComponent<Enemy_Health>();

        dismissState = new GolluxSummon_DismissState(EParamenter_Enemy.isDeath.ToString(), stateMachine, this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        isDismiss = false;
    }

    public override void OnDead() => DismissSummon(out float currentHealth);

    public void DismissSummon(out float currentHealth)
    {
        currentHealth = enemyHealth.currentHealth;

        // Change DismissState
        isDismiss = true;
        stateMachine.ChangeState(dismissState);
    }
}
