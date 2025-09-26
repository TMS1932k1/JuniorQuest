using UnityEngine;

public class Bat_SFX : Entity_SFX
{
    public override void PlayAttackHit()
    {
        PlayAudioClip(ClipDataNameStrings.BAT_ATTACK_HIT);
    }

    public override void PlayAttackMiss()
    {
        PlayAudioClip(ClipDataNameStrings.BAT_ATTACK_MISS);
    }

    public override void PlayDeath()
    {
        PlayAudioClip(ClipDataNameStrings.BAT_DEATH);
    }
}
