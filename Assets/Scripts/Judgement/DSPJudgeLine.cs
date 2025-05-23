using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class DSPJudgeLine : MonoBehaviour
{
    public KeyCode[] inputKeys; // 여러 키 입력 가능
    public JudgementWindow window;
    public DSPNoteManager noteManager;
    public int judgeLineIndex; // 담당 라인 번호 (0~2 등)

    private List<INote> notesInZone = new List<INote>();
    private LongNote activeHoldNote = null; // 현재 유지 중인 롱노트
    private bool isHolding = false;
    public GameObject JudgeLineLight;

    public TextMeshProUGUI judgementText;
    private Coroutine judgementRoutine;

    void Update()
    {
        double now = noteManager.GetMusicTime();
        double adjustedTime = now; //+ noteManager.fallDelay;

        // 유지 입력 처리 (롱노트 완료 or 실패 판정)
        if (activeHoldNote != null)
        {
            bool keyHeld = inputKeys.Any(key => Input.GetKey(key));

            if (keyHeld)
            {
                if (adjustedTime >= activeHoldNote.TargetTime + activeHoldNote.duration)
                {
                    Debug.Log("LONG NOTE SUCCESS");
                    notesInZone.Remove(activeHoldNote);
                    Destroy((activeHoldNote as MonoBehaviour).gameObject);
                    activeHoldNote = null;
                    isHolding = false;
                }
            }
            else if (isHolding) // 키를 놓은 경우 실패 처리
            {
                JudgeLineLight.gameObject.SetActive(false);
                Debug.Log("LONG NOTE MISS (중간에 놓음)");
                notesInZone.Remove(activeHoldNote);
                Destroy((activeHoldNote as MonoBehaviour).gameObject);
                activeHoldNote = null;
                isHolding = false;
            }
        }
        // 입력 판정 (숏노트, 롱노트 시작)
        foreach (var key in inputKeys)
        {     
            if (Input.GetKeyDown(key))
            {
                JudgeLineLight.SetActive(true);
                INote closest = notesInZone
                    .Where(n => n.Line == judgeLineIndex)
                    .OrderBy(n => Math.Abs(adjustedTime - n.TargetTime))
                    .FirstOrDefault();

                if (closest == null) return;

                double delta = Math.Abs(adjustedTime - closest.TargetTime);

                if (delta <= window.perfect)
                    ShowJudgement("PERFECT");
                else if (delta <= window.excellent)
                    ShowJudgement("EXCELLENT");
                else if (delta <= window.good)
                    ShowJudgement("GOOD");
                else
                    ShowJudgement("MISS");
                Debug.Log($"[{key}] MISS - delta = {delta:F4}s (Target: {closest.TargetTime:F3}, Now: {adjustedTime:F3})");

                if (closest is LongNote hold)
                {
                    activeHoldNote = hold;
                    isHolding = true;
                }
                else
                {
                    Destroy((closest as MonoBehaviour).gameObject);
                    notesInZone.Remove(closest);
                }

                break;
            }
            if (Input.GetKey(key))
            {
                JudgeLineLight.SetActive(true);
            }
            else
            {
                JudgeLineLight.SetActive(false);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        var note = other.GetComponent<INote>();
        if (note != null && note.Line == judgeLineIndex)
            notesInZone.Add(note);
    }

    void OnTriggerExit(Collider other)
    {
        var note = other.GetComponent<INote>();
        if (note != null && notesInZone.Contains(note))
        {
            Debug.Log($"MISS (지남)");
            notesInZone.Remove(note);
            if ((object)note != (object)activeHoldNote)
                Destroy((note as MonoBehaviour).gameObject);

            if ((object)note == (object)activeHoldNote)
            {
                activeHoldNote = null;
                isHolding = false;
            }
        }
    }

    void ShowJudgement(string text)
    {
        if (judgementRoutine != null)
            StopCoroutine(judgementRoutine);

        judgementRoutine = StartCoroutine(DisplayJudgement(text));
    }
    IEnumerator DisplayJudgement(string text)
    {
        judgementText.text = text;
        judgementText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        judgementText.gameObject.SetActive(false);
    }
}
