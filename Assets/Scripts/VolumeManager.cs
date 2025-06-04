using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;

    [Header("Mixer & Targets")]
    public AudioMixer mixer;
    public string masterParam = "MasterVolume";
    public string bgmParam = "BGMVolume";
    public string sfxParam = "SFXVolume";

    [Range(0.0001f, 1f)] public float master = 1f;
    [Range(0.0001f, 1f)] public float bgm = 1f;
    [Range(0.0001f, 1f)] public float sfx = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolume();
        ApplyVolume();
    }

    public void SetVolumes(float m, float b, float s)
    {
        master = m;
        bgm = b;
        sfx = s;
        ApplyVolume();
        SaveVolume();
    }

    public void ApplyVolume()
    {
        mixer.SetFloat(masterParam, LinearToDb(master));
        mixer.SetFloat(bgmParam, LinearToDb(master * bgm));
        mixer.SetFloat(sfxParam, LinearToDb(master * sfx));
    }

    float LinearToDb(float val) => Mathf.Log10(Mathf.Clamp(val, 0.0001f, 1f)) * 20f;

    void SaveVolume()
    {
        PlayerPrefs.SetFloat("Vol_Master", master);
        PlayerPrefs.SetFloat("Vol_BGM", bgm);
        PlayerPrefs.SetFloat("Vol_SFX", sfx);
    }

    void LoadVolume()
    {
        master = PlayerPrefs.GetFloat("Vol_Master", 1f);
        bgm = PlayerPrefs.GetFloat("Vol_BGM", 1f);
        sfx = PlayerPrefs.GetFloat("Vol_SFX", 1f);
    }
}