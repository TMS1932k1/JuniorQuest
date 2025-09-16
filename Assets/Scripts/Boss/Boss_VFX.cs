using UnityEngine;

public class Boss_VFX : Enemy_VFX
{
    [Header("Death")]
    [SerializeField] Color deathColor = Color.gray;

    public void PlayDeathVFX()
    {
        sr.color = deathColor;
    }
}
