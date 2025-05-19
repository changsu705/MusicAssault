using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPNoteManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextAsset chartJSON;
    public GameObject notePrefab;
    public GameObject longNotePrefab;
    public Transform[] lanes;
    public float fallDelay = 2f;

    private NoteChart chart;
    private int currentIndex = 0;
    private double dspStartTime;
    private List<INote> activeNotes = new List<INote>();

    void Start()
    {
        chart = JsonUtility.FromJson<NoteChart>(chartJSON.text);
        StartCoroutine(LoadAndScheduleAudio());
    }

    IEnumerator LoadAndScheduleAudio()
    {
        audioSource.clip.LoadAudioData();
        while (audioSource.clip.loadState != AudioDataLoadState.Loaded)
            yield return null;

        dspStartTime = AudioSettings.dspTime + 1.0;
        audioSource.PlayScheduled(dspStartTime);
    }

    void Update()
    {
        double now = AudioSettings.dspTime - dspStartTime;

        while (currentIndex < chart.notes.Count)
        {
            var note = chart.notes[currentIndex];
            if (now >= note.time - fallDelay)
            {
                if (note.line < 0 || note.line >= lanes.Length)
                {
                    Debug.LogWarning($"note.line {note.line} out of range.");
                    currentIndex++;
                    continue;
                }

                GameObject obj = (note.duration > 0f)
                    ? Instantiate(longNotePrefab, lanes[note.line].position, Quaternion.identity)
                    : Instantiate(notePrefab, lanes[note.line].position, Quaternion.identity);

                INote noteComponent = obj.GetComponent<INote>();
                if (noteComponent != null)
                {
                    noteComponent.Initialize(note.time, note.duration);
                    activeNotes.Add(noteComponent);
                }
                else
                {
                    Debug.LogError("? INote 컴포넌트가 연결되지 않은 프리팹입니다.");
                }

                currentIndex++;
            }
            else break;
        }

        foreach (var note in activeNotes)
            note.Tick();
    }

    public double GetMusicTime()
    {
        return AudioSettings.dspTime - dspStartTime;
    }
}