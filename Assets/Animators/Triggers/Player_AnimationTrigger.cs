using UnityEngine;

public class Player_AnimationTrigger : Entity_AnimationTrigger
{
    /// <summary>
    /// Set index to get new attack circle in (Entity_Combat)
    /// </summary>
    /// <param name="index">Index of current basic attack</param>
    protected void SetComboIndex(int index)
    {
        entityCombat.SetAttackCircleIndex(index);
    }
}
