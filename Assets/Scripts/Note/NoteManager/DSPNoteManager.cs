using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DSPNoteManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextAsset chartJSON;
    public GameObject shootNotePrefab;
    public GameObject longNotePrefab;
    public Transform[] lanes;
    public float fallDelay = 2f;
    public float preStartDelay = 5f;
    public TextMeshProUGUI countdownText;

    private NoteChart chart;
    private int currentIndex = 0;
    private double dspStartTime;
    private List<INote> activeNotes = new List<INote>();
    private bool gameStarted = false;

    void Start()
    {
        StartCoroutine(CountdownThenStart());
    }

    IEnumerator CountdownThenStart()
    {
        float countdown = preStartDelay;
        while (countdown > 0)
        {
            countdownText.text = Mathf.CeilToInt(countdown).ToString();
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }

        countdownText.text = "START!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        chart = JsonUtility.FromJson<NoteChart>(chartJSON.text);
        dspStartTime = AudioSettings.dspTime + fallDelay;
        audioSource.PlayScheduled(dspStartTime);
        gameStarted = true;
    }

    void Update()
    {
        if (!gameStarted)
            return;

        double now = AudioSettings.dspTime - dspStartTime;

        // 노트 생성
        while (currentIndex < chart.notes.Count)
        {
            var noteData = chart.notes[currentIndex];
            if (now >= noteData.time - fallDelay)
            {
                Transform spawnPos = lanes[noteData.line];
                GameObject prefab = noteData.duration > 0 ? longNotePrefab : shootNotePrefab;
                GameObject obj = Instantiate(prefab, spawnPos.position, Quaternion.identity);
                INote noteComponent = obj.GetComponent<INote>();
                noteComponent.Initialize(noteData.time, noteData.duration, noteData.line);
                activeNotes.Add(noteComponent);
                currentIndex++;
            }
            else break;
        }

        // Tick
        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            var mono = activeNotes[i] as MonoBehaviour;
            if (mono == null)
            {
                activeNotes.RemoveAt(i);
                continue;
            }
            activeNotes[i].Tick();
        }
    }

    public double GetMusicTime()
    {
        return AudioSettings.dspTime - dspStartTime;
    }
}
