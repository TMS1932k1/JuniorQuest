using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void CallTrigger()
    {
        player.CallTriggerCurrentState();
    }
}
