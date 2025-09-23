using UnityEngine;

public class Golem_SFX : Entity_SFX
{
    public override void PlayAttackHit()
    {
        PlayAudioClip(ClipDataNameStrings.GOLEM_ATTACK_HIT);
    }

    public override void PlayAttackMiss()
    {
        PlayAudioClip(ClipDataNameStrings.GOLEM_ATTACK_MISS);
    }

    public override void PlayDeath()
    {
        PlayAudioClip(ClipDataNameStrings.GOLEM_DEATH);
    }
}
