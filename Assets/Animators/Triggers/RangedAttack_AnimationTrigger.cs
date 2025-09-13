using UnityEngine;

public class RangedAttack_AnimationTrigger : MonoBehaviour
{
    private FlyDemon_RangedAttack rangedAttack;

    void Awake()
    {
        rangedAttack = GetComponentInParent<FlyDemon_RangedAttack>();
    }

    private void Hide()
    {
        rangedAttack.Hide();
    }
}
