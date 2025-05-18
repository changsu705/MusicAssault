using System;
using System.Collections.Generic;
using UnityEngine;

public class DSPJudgeLine : MonoBehaviour
{
    public KeyCode[] keys;
    public JudgementWindow window;
    public DSPNoteManager noteManager;

    private List<Note> notesInZone = new List<Note>();

    void Update()
    {
        if (notesInZone.Count == 0) return;

        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                Note closest = notesInZone[0];
                double inputTime = noteManager.GetMusicTime();
                double delta = Math.Abs(inputTime - closest.targetTime);

                if (delta <= window.perfect)
                    Debug.Log("PERFECT");
                else if (delta <= window.excellent)
                    Debug.Log("EXCELLENT");
                else if (delta <= window.good)
                    Debug.Log("GOOD");
                else
                    Debug.Log("MISS");

                Destroy(closest.gameObject);
                notesInZone.Remove(closest);
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
            notesInZone.Add(other.GetComponent<Note>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            Note note = other.GetComponent<Note>();
            if (notesInZone.Contains(note))
            {
                Debug.Log("MISS (지남)");
                notesInZone.Remove(note);
                Destroy(note.gameObject);
            }
        }
    }
}
// 나중에 의존성 역전법칙 적용하여 다시 코딩할 것 화요일 정도