using System.Collections;
using UnityEngine;

public class Player_SFX : Entity_SFX
{
    private Coroutine moveCoroutine;

    public override void PlayAttackHit()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_ATTACK_HIT);
    }

    public override void PlayAttackMiss()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_ATTACK_MISS);
    }

    public override void PlayDeath()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_DEATH);
    }

    public void PlayJump()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_JUMP);
    }

    public void PlayLand()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_LAND);
    }

    public void PlayDash()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_DASH);
    }

    public void PlaySlide()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_SLIDE);
    }

    public void PlayCounter()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_COUNTER);
    }

    public void PlayBuff()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_BUFF);
    }

    public void PlayFireBlade()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_FIRE_BLADE);
    }

    public void PlayInvisibility()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_INVISIBILITY);
    }

    public void PlayBattleCry()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_BATTLE_CRY);
    }

    public void PlayIcePrison()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_ICE_PRISON);
    }

    public void PlayComeback()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_COMEBACK);
    }

    public void PlayLevelUP()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_LEVEL_UP);
    }

    public void PlayShieldBarrier()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_SHIELD_BARRIER);
    }

    public void PlayHurt()
    {
        PlayAudioClip(ClipDataNameStrings.PLAYER_HURT);
    }

    public void PlayMove(float interval)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(moveCo(interval));
    }

    public void StopMove()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    private IEnumerator moveCo(float interval)
    {
        while (true)
        {
            PlayAudioClip(ClipDataNameStrings.PLAYER_MOVE);
            yield return new WaitForSeconds(interval);
        }
    }
}
