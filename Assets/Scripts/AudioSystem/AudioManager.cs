using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSO audioSO;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioClip(AudioSource audioSource, string dataName)
    {
        AudioClipsData clipsData = audioSO.GetAudioClip(dataName);
        if (clipsData == null)
        {
            Debug.LogWarning("AUDIO_MANAGER: Clips data is null or empty!");
            return;
        }

        audioSource.volume = clipsData.volume;
        audioSource.PlayOneShot(clipsData.GetRandomClip());
    }
}
