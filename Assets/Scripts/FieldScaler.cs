using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScaler : MonoBehaviour
{
    [Header("ũ�� ��ȯ�ӵ� ����")]
    public Vector3 targetScale = new Vector3(1, 1, 12f);
    public float duration = 2f; // �ٲ�� �ð�
    public float curvePower = 2f;  // ���� ���

    [Header("�ִ� ũ��")]
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

            // Ŀ�� ����: 0.5 ���� �� ���� ���� / 2 �̻� �� ���� ����
            float weightedT = Mathf.Pow(t, curvePower);

            transform.localScale = Vector3.Lerp(startScale, targetScale, weightedT);
        }
    }
}
