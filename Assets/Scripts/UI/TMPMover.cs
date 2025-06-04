using System.Collections;
using UnityEngine;

public class TMPMover : MonoBehaviour
{
    public RectTransform targetTMP;
    public Vector2 destination;
    public float moveDuration = 1f;

    public void MoveTMP()
    {
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        Vector2 start = targetTMP.anchoredPosition;
        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            targetTMP.anchoredPosition = Vector2.Lerp(start, destination, time / moveDuration);
            yield return null;
        }

        targetTMP.anchoredPosition = destination;
    }
}