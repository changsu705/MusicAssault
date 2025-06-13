using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;

    public AudioMixer mixer;
    public string masterParam = "MasterVolume";
    public string bgmParam = "BGMVolume";
    public string sfxParam = "SFXVolume";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetMasterVolume(float value)
    {
        SetVolume(masterParam, value);
    }

    public void SetBGMVolume(float value)
    {
        SetVolume(bgmParam, value);
    }

    public void SetSFXVolume(float value)
    {
        SetVolume(sfxParam, value);
    }

    private void SetVolume(string paramName, float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(paramName, dB);
    }
}
