using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScaler : MonoBehaviour
{
    [Header("크기 전환속도 조정")]
    public Vector3 targetScale = new Vector3(1, 1, 12f);
    public float duration = 2f; // 바뀌는 시간
    public float curvePower = 2f;  // 비선형 상수

    [Header("최대 크기")]
    private Vector3 startScale;
    private float elapsedTime = 0f;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // 커브 적용: 0.5 이하 → 감속 시작 / 2 이상 → 빠른 가속
            float weightedT = Mathf.Pow(t, curvePower);

            transform.localScale = Vector3.Lerp(startScale, targetScale, weightedT);
        }
    }
}
