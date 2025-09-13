using UnityEngine;

public class Gollux_SkillManager : MonoBehaviour
{
    private Gollux gollux;
    private Gollux_SkillRockDrop rockDrop;
    private Gollux_SkillNormalAttack normalAttack;

    private void Awake()
    {
        gollux = GetComponentInParent<Gollux>();
        rockDrop = GetComponentInChildren<Gollux_SkillRockDrop>();
        normalAttack = GetComponentInChildren<Gollux_SkillNormalAttack>();
    }

    public void PerformRockDrop()
    {
        gollux.anim.Play(EAnim_Gollux.Gollux_RockDrop.ToString());
        rockDrop.Perform();
    }

    public void PerformNormalAttack()
    {
        gollux.anim.Play(EAnim_Gollux.Gollux_NormalAttack.ToString());
    }
}
