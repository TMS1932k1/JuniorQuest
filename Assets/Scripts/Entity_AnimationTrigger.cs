using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{
    protected Entity entity;
    protected Entity_Combat entityCombat;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    protected void CurrentStateTrigger()
    {
        entity.CallTriggerCurrentState();
    }

    protected void AttackTrigger()
    {
        entityCombat.PerformAttack();
    }
}
