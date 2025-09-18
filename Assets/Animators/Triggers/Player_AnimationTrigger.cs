using UnityEngine;

public class Player_AnimationTrigger : Entity_AnimationTrigger
{
    private Player_Combat playerCombat;

    protected override void Awake()
    {
        base.Awake();

        playerCombat = GetComponentInParent<Player_Combat>();
    }

    /// <summary>
    /// Set index to get new attack circle in (Entity_Combat)
    /// </summary>
    /// <param name="index">Index of current basic attack</param>
    protected void SetComboIndex(int index)
    {
        playerCombat.SetAttackCircleIndex(index);
    }
}
