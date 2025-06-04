using UnityEngine;
using System.Collections;

public class MaterialEffectManager : MonoBehaviour
{
    /// <summary>
    /// ������ Renderer�� ��Ƽ������ ���������� ��ü�ߴٰ�, ���� �ð� �� ������ ��Ƽ����� ����.
    /// </summary>
    public void SwapMaterialWithShake(Renderer targetRenderer, Material redMat, Material restoreMat, Transform shakeTarget, float duration = 1f, float shakeAmount = 0.1f)
    {
        StartCoroutine(SwapAndShakeCoroutine(targetRenderer, redMat, restoreMat, shakeTarget, duration, shakeAmount));
    }

    private IEnumerator SwapAndShakeCoroutine(Renderer rend, Material redMat, Material restoreMat, Transform shakeTarget, float duration, float shakeAmount)
    {
        if (rend == null || redMat == null || restoreMat == null) yield break;

        //  ��Ƽ���� ��ü
        rend.material = redMat;

        //  ���� ����
        Vector3 originalPos = shakeTarget != null ? shakeTarget.localPosition : Vector3.zero;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (shakeTarget != null)
            {
                Vector3 offset = Random.insideUnitSphere * shakeAmount;
                offset.y = 0f; // ���� ����
                shakeTarget.localPosition = originalPos + offset;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (shakeTarget != null)
            shakeTarget.localPosition = originalPos;

        //  ���� ��Ƽ����� ��ü
        rend.material = restoreMat;
    }
}
