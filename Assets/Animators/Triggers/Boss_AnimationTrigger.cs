using UnityEngine;

public class Boss_AnimationTrigger : MonoBehaviour
{
    private Boss_CommandManager commandManager;
    private Entity_Combat combat;

    private void Awake()
    {
        commandManager = GetComponentInParent<Boss_CommandManager>();
        combat = GetComponentInParent<Entity_Combat>();
    }

    private void CallTriggerCurrentCommand()
    {
        commandManager.CallTrigger();
    }

    private void AttackTrigger()
    {
        combat.PerformAttack();
    }
}
