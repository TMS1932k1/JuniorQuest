using UnityEngine;

public class Skill_Slash_AnimationTrigger : MonoBehaviour
{
    private Skill_FireBlade_Slash slash;

    void Awake()
    {
        slash = GetComponentInParent<Skill_FireBlade_Slash>();
    }

    public void Hide()
    {
        slash.Hide();
    }
}
