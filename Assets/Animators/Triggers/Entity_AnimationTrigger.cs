using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{
    protected Entity entity;
    protected ICombat entityCombat;


    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<ICombat>();
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
