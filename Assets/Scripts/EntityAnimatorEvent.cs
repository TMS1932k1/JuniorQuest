using UnityEngine;

public class EntityAnimatorEvent : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CallTrigger()
    {
        entity.CallTriggerCurrentState();
    }
}
