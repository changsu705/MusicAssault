using UnityEngine;
using System.Collections;

public class MaterialEffectManager : MonoBehaviour
{
    /// <summary>
    /// 지정된 Renderer의 머티리얼을 빨간색으로 교체했다가, 일정 시간 후 복구용 머티리얼로 변경.
    /// </summary>
    public void SwapMaterialWithShake(Renderer targetRenderer, Material redMat, Material restoreMat, Transform shakeTarget, float duration = 1f, float shakeAmount = 0.1f)
    {
        StartCoroutine(SwapAndShakeCoroutine(targetRenderer, redMat, restoreMat, shakeTarget, duration, shakeAmount));
    }

    private IEnumerator SwapAndShakeCoroutine(Renderer rend, Material redMat, Material restoreMat, Transform shakeTarget, float duration, float shakeAmount)
    {
        if (rend == null || redMat == null || restoreMat == null) yield break;

        //  머티리얼 교체
        rend.material = redMat;

        //  흔들기 시작
        Vector3 originalPos = shakeTarget != null ? shakeTarget.localPosition : Vector3.zero;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (shakeTarget != null)
            {
                Vector3 offset = Random.insideUnitSphere * shakeAmount;
                offset.y = 0f; // 수평만 흔들기
                shakeTarget.localPosition = originalPos + offset;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (shakeTarget != null)
            shakeTarget.localPosition = originalPos;

        //  복구 머티리얼로 교체
        rend.material = restoreMat;
    }
}
