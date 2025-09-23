using UnityEngine;

public abstract class Entity_SFX : MonoBehaviour
{
    protected AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    protected void PlayAudioClip(string clipName)
    {
        AudioManager.instance.PlayAudioClip(audioSource, clipName);
    }

    public abstract void PlayAttackHit();
    public abstract void PlayAttackMiss();
    public abstract void PlayDeath();
}
