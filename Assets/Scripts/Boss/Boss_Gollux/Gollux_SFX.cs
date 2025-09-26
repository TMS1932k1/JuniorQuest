using UnityEngine;

public class Gollux_SFX : Entity_SFX
{
    public override void PlayAttackHit()
    {
        PlayAudioClip(ClipDataNameStrings.GOLLUX_ATTACK_HIT);
    }

    public override void PlayAttackMiss()
    {
        PlayAudioClip(ClipDataNameStrings.GOLLUX_ATTACK_MISS);
    }

    public override void PlayDeath()
    {
        PlayAudioClip(ClipDataNameStrings.GOLLUX_DEATH);
    }
}
