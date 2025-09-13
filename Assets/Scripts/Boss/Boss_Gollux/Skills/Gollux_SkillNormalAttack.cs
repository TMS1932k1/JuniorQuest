using UnityEngine;

public class Gollux_SkillNormalAttack : MonoBehaviour
{
    private Entity_Combat combat;


    private void Awake()
    {
        combat = GetComponentInParent<Entity_Combat>();
    }
}
