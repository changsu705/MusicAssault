using UnityEngine;
using UnityEngine.UI;

public class AudioHaloUI : MonoBehaviour
{
    public GameObject barPrefab;
    public int barCount = 8;
    public float maxHeight = 300f;
    public float radius = 200f;              // 원형 반지름
    public Color lowColor = Color.green;
    public Color highColor = Color.red;

    private RectTransform[] bars;
    private Image[] barImages;

    void Start()
    {
        bars = new RectTransform[barCount];
        barImages = new Image[barCount];

        for (int i = 0; i < barCount; i++)
        {
            GameObject bar = Instantiate(barPrefab, transform);
            bar.name = "Bar" + i.ToString("00");

            RectTransform rt = bar.GetComponent<RectTransform>();

            // 각도를 계산하여 원형 배치
            float angle = i * (360f / barCount);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
            rt.anchoredPosition = pos;

            // 막대가 바깥을 향하도록 회전
            rt.localRotation = Quaternion.Euler(0, 0, angle);

            bars[i] = rt;
            barImages[i] = bar.GetComponent<Image>();
        }
    }

    void Update()
    {
        for (int i = 0; i < barCount; i++)
        {
            float value = Mathf.Clamp01(AudioPeer.bandBuffet[i]);
            float length = value * maxHeight;

            if (bars[i] != null)
            {
                // 막대를 value에 따라 길게 (Y축만)
                bars[i].sizeDelta = new Vector2(bars[i].sizeDelta.x, Mathf.Max(length, 10f));
            }

            if (barImages[i] != null)
            {
                barImages[i].color = Color.Lerp(lowColor, highColor, value);
            }
        }
    }
}
