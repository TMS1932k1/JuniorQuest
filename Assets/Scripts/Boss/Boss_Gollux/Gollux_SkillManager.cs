using UnityEngine;

public class Gollux_SkillManager : MonoBehaviour
{
    private Gollux gollux;
    private Gollux_SkillRockDrop rockDrop;

    private void Awake()
    {
        gollux = GetComponentInParent<Gollux>();
        rockDrop = GetComponentInChildren<Gollux_SkillRockDrop>();
    }

    public void PerformRockDrop()
    {
        rockDrop.Perform();
    }
}
