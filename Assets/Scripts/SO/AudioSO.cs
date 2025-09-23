using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Data Setup/Create new Audio Data", fileName = "AUDIO_DATA_NEW")]
public class AudioSO : ScriptableObject
{
    [SerializeField] List<AudioClipsData> player;
    [SerializeField] List<AudioClipsData> rangeAttack;
    [SerializeField] List<AudioClipsData> golem;
    [SerializeField] List<AudioClipsData> bgm;

    [SerializeField] Dictionary<string, AudioClipsData> audioClipsCollection = new();


    private void OnValidate()
    {
        AddAudioCLip(player);
        AddAudioCLip(rangeAttack);
        AddAudioCLip(golem);
        AddAudioCLip(bgm);
    }

    public AudioClipsData GetAudioClip(string dataName)
    {
        return audioClipsCollection.TryGetValue(dataName, out AudioClipsData audioClips) ? audioClips : null;
    }

    private void AddAudioCLip(List<AudioClipsData> clipsDatas)
    {
        foreach (AudioClipsData clipsData in clipsDatas)
        {
            if (clipsData != null && !audioClipsCollection.ContainsKey(clipsData.dataName))
            {
                audioClipsCollection.Add(clipsData.dataName, clipsData);
            }
        }
    }
}

[System.Serializable]
public class AudioClipsData
{
    public string dataName;
    [Range(0, 1)]
    public float volume;
    public List<AudioClip> audioClips = new();

    private AudioClip lastAudioClip;

    public AudioClip GetRandomClip()
    {
        if (audioClips == null || audioClips.Count <= 0)
        {
            Debug.LogWarning("AUDIO_MANAGER: Audio clip data is null or empty!");
            return null;
        }

        AudioClip randomClip = null;

        do
            randomClip = audioClips[Random.Range(0, audioClips.Count)];
        while (audioClips.Count > 1 && randomClip == lastAudioClip);

        lastAudioClip = randomClip;

        return randomClip;
    }
}