using UnityEngine;

public class Gollux_SkillManager : MonoBehaviour
{
    private Gollux_SkillRockDrop rockDrop;

    private void Awake()
    {
        rockDrop = GetComponentInChildren<Gollux_SkillRockDrop>();
    }

    public void PerformSkillAttack1()
    {
        rockDrop.Perform();
    }
}
