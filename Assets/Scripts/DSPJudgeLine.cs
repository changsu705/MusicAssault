using System;
using System.Collections.Generic;
using UnityEngine;

public class DSPJudgeLine : MonoBehaviour
{
    public KeyCode[] keys;
    public JudgementWindow window;
    public DSPNoteManager noteManager;

    private List<INote> notesInZone = new List<INote>();


    void Update()
    {
        if (notesInZone.Count == 0) return;

        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                INote closest = notesInZone[0];
                double inputTime = noteManager.GetMusicTime();
                double delta = Math.Abs(inputTime - closest.TargetTime);

                if (delta <= window.perfect)
                    Debug.Log("PERFECT");
                else if (delta <= window.excellent)
                    Debug.Log("EXCELLENT");
                else if (delta <= window.good)
                    Debug.Log("GOOD");
                else
                    Debug.Log("MISS");

                // MonoBehaviour로 캐스팅 후 제거
                MonoBehaviour mono = closest as MonoBehaviour;
                if (mono != null)
                    Destroy(mono.gameObject);

                notesInZone.Remove(closest);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var note = other.GetComponent<INote>();
        if (note != null)
            notesInZone.Add(note);
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            INote note = other.GetComponent<INote>();
            if (note != null && notesInZone.Contains(note))
            {
                Debug.Log("MISS (지남)");
                notesInZone.Remove(note);

                MonoBehaviour mono = note as MonoBehaviour;
                if (mono != null)
                    Destroy(mono.gameObject);
            }
        }
    }
}
// 나중에 의존성 역전법칙 적용하여 다시 코딩할 것 화요일 정도