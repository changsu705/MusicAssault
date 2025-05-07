using System.Collections.Generic;
using UnityEngine;

public class JudgeLine : MonoBehaviour
{
    public KeyCode[] keys;
    public JudgementWindow window;

    private List<Note> notesInZone = new List<Note>();

    void Update()
    {
        if (notesInZone.Count == 0) return;

        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                Note closest = notesInZone[0];
                float delta = Mathf.Abs(Time.time - closest.targetTime);

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
                Debug.Log("MISS (Áö³²)");
                notesInZone.Remove(note);
                Destroy(note.gameObject);
            }
        }
    }
}