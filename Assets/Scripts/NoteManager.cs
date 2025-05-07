using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public TextAsset chartJSON;
    public GameObject notePrefab;
    public Transform[] lanes;
    public float fallDelay = 2f;

    private NoteChart chart;
    private int currentIndex = 0;

    void Start()
    {
        chart = JsonUtility.FromJson<NoteChart>(chartJSON.text);
    } 

    void Update()
    {
        float now = Time.time;

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
}