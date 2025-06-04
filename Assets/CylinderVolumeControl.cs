using UnityEngine;
using UnityEngine.Audio;

public class CylinderVolumeControl : MonoBehaviour
{
    public AudioMixer mixer;

    [Header("Cylinder Targets")]
    public Transform mainCylinder;
    public Transform bgmCylinder;
    public Transform sfxCylinder;

    [Header("Scale Range")]
    public float minScaleY = 0.1f;
    public float maxScaleY = 1.0f;

    void Update()
    {
        // 개별 비율
        float masterVolume = Mathf.InverseLerp(minScaleY, maxScaleY, mainCylinder.localScale.y);
        float bgmRaw = Mathf.InverseLerp(minScaleY, maxScaleY, bgmCylinder.localScale.y);
        float sfxRaw = Mathf.InverseLerp(minScaleY, maxScaleY, sfxCylinder.localScale.y);

        // 최종 볼륨 = 마스터 곱하기 개별
        float bgmFinal = masterVolume * bgmRaw;
        float sfxFinal = masterVolume * sfxRaw;

        mixer.SetFloat("BGMVolume", LinearToDecibel(bgmFinal));
        mixer.SetFloat("SFXVolume", LinearToDecibel(sfxFinal));
        mixer.SetFloat("MasterVolume", LinearToDecibel(masterVolume)); // Mixer에 따로 Master 노출하고 싶을 때
    }

    float LinearToDecibel(float linear)
    {
        if (linear <= 0.0001f)
            return -80f;
        return Mathf.Log10(linear) * 20f;
    }
}

