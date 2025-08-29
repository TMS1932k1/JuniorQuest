using System.Collections;
using UnityEngine;

public class Skill_ShieldBarrier : Skill_Base
{
    private Player_Health playerHealth;

    private Coroutine CreateShieldCoroutine;

    protected override void Awake()
    {
        base.Awake();

        playerHealth = GetComponentInParent<Player_Health>();
    }

    public override void PerformSkill()
    {
        base.PerformSkill();

        SetLastTimeUsed(); // Cooldowntimer
        playerVFX.ShowShieldBarrierVFX(skillData.duration); // VFX
        ReduceTakeDamge();
    }

    private void ReduceTakeDamge()
    {
        if (CreateShieldCoroutine != null)
            StopCoroutine(CreateShieldCoroutine);

        CreateShieldCoroutine = StartCoroutine(CreateShieldCo());
    }

    private IEnumerator CreateShieldCo()
    {
        playerHealth.SetReduceDamagePercent(skillData.effectPercent);
        yield return new WaitForSeconds(skillData.duration);
        playerHealth.SetReduceDamagePercent(0);
    }
}
