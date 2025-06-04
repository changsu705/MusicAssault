using System.Collections;
using UnityEngine;

public class TMPFader : MonoBehaviour
{
    public CanvasGroup[] targetGroups;   // 페이드 대상 TMP 텍스트들의 CanvasGroup
    public float fadeDuration = 1f;
    public GameObject TMP;               // 외부에서 따로 지정한 TMP 묶음 (이건 상태만 켜는 용도)
    private bool hasPlayed = false;

    void Start()
    {
        foreach (var group in targetGroups)
        {
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }

    public void ShowTMP()
    {
        if (hasPlayed) return;
        StartCoroutine(FadeInAll());
    }

    IEnumerator FadeInAll()
    {
        float time = 0f;

        TMP.SetActive(false);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);

            foreach (var group in targetGroups)
            {
                group.alpha = alpha;
            }

            yield return null;
        }

        foreach (var group in targetGroups)
        {
            group.alpha = 1f;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }
}
