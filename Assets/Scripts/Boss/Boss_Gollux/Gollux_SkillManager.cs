using UnityEngine;

public class Gollux_SkillManager : MonoBehaviour
{
    public Gollux gollux { get; private set; }
    public Gollux_SkillRockDrop rockDrop { get; private set; }
    public Gollux_SkillSummon summon { get; private set; }

    private void Awake()
    {
        gollux = GetComponentInParent<Gollux>();
        rockDrop = GetComponentInChildren<Gollux_SkillRockDrop>();
        summon = GetComponentInChildren<Gollux_SkillSummon>();
    }
}
