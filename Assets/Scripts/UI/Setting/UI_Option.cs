using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Option : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;


    [Header("Slider Settings")]
    [SerializeField] Slider effectsSlider;
    [SerializeField] string effectsParam;

    [Space]
    [SerializeField] Slider uiSlider;
    [SerializeField] string uiParam;

    [Space]
    [SerializeField] Slider bgmSlider;
    [SerializeField] string bgmParam;


    private void Start()
    {
        effectsSlider.value = SaveManager.instance.GetGameData().effectsValue;
        uiSlider.value = SaveManager.instance.GetGameData().uiValue;
        bgmSlider.value = SaveManager.instance.GetGameData().bgmValue;
    }

    public void SetVolumeEffects(float value)
    {
        SetAudioMixer(effectsParam, value);
        SaveManager.instance.SaveEffectsAudio(value);
    }

    public void SetVolumeUI(float value)
    {
        SetAudioMixer(uiParam, value);
        SaveManager.instance.SaveUIAudio(value);
    }

    public void SetVolumeBGM(float value)
    {
        SetAudioMixer(bgmParam, value);
        SaveManager.instance.SaveBgmAudio(value);
    }

    private void SetAudioMixer(string param, float value)
    {
        audioMixer.SetFloat(param, ChangeToDBMixer(value));
    }

    /// <summary>
    /// 1 value = 20 dB
    /// </summary>
    /// <param name="value">Value of Slider</param>
    /// <returns></returns>
    private float ChangeToDBMixer(float value)
    {
        return (value - 0.8f) * 100;
    }
}
