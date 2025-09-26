using UnityEngine;

public class FlyDemon_SFX : Entity_SFX
{
    public override void PlayAttackHit() { } // Don't need because FlyDemon Attack is range attack

    public override void PlayAttackMiss() { } // Don't need because FlyDemon Attack is range attack

    public override void PlayDeath()
    {
        PlayAudioClip(ClipDataNameStrings.FLY_DEMON_DEATH);
    }
}
