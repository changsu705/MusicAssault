using System.Collections;
using UnityEngine;

public class DSPNoteManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextAsset chartJSON;
    public GameObject notePrefab;
    public Transform[] lanes;
    public float fallDelay = 2f;

    private NoteChart chart;
    private int currentIndex = 0;
    private double dspStartTime;

    void Start()
    {
        chart = JsonUtility.FromJson<NoteChart>(chartJSON.text);

        // 오디오 프리로딩이 필요할 경우
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
                Transform spawnPos = lanes[note.line];
                GameObject obj = Instantiate(notePrefab, spawnPos.position, Quaternion.identity);
                obj.GetComponent<Note>().targetTime = note.time;
                currentIndex++;
            }
            else break;
        }
    }

    public double GetMusicTime()
    {
        return AudioSettings.dspTime - dspStartTime;
    }
}