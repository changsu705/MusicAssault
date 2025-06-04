using UnityEngine;

public class CylinderVolumeUI : MonoBehaviour
{
    public RectTransform masterCylinder;
    public RectTransform bgmCylinder;
    public RectTransform sfxCylinder;

    public float minY = 0.1f;
    public float maxY = 1.0f;

    void Start()
    {
        UpdateCylinderHeight(VolumeManager.Instance.master, masterCylinder);
        UpdateCylinderHeight(VolumeManager.Instance.bgm, bgmCylinder);
        UpdateCylinderHeight(VolumeManager.Instance.sfx, sfxCylinder);
    }

    void Update()
    {
        float m = GetVolumeFromCylinder(masterCylinder);
        float b = GetVolumeFromCylinder(bgmCylinder);
        float s = GetVolumeFromCylinder(sfxCylinder);

        VolumeManager.Instance.SetVolumes(m, b, s);
    }

    float GetVolumeFromCylinder(RectTransform t)
    {
        return Mathf.InverseLerp(minY, maxY, t.localScale.y);
    }

    void UpdateCylinderHeight(float volume, RectTransform t)
    {
        Vector3 s = t.localScale;
        s.y = Mathf.Lerp(minY, maxY, volume);
        t.localScale = s;
    }
}