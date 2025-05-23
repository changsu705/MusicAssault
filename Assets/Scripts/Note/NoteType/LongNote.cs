using UnityEngine;

public class LongNote : MonoBehaviour, INote
{
    public float targetTime;
    public float duration;
    public float speed = 5f;
    public Transform body;
    private Vector3 startPos;
    private double spawnDSPTime;
    public int Line { get; private set; }

    public float TargetTime => targetTime;

    public void Initialize(float time, float duration = 0f, int line = 0)
    {
        targetTime = time;
        this.duration = duration;
        Line = line;
        startPos = transform.position;
        spawnDSPTime = AudioSettings.dspTime;

        float length = speed * duration;
        if (body != null)
        {
            body.localScale = new Vector3(0.1f, 1, length);
            body.localPosition = new Vector3(0, 0, length / 2f);
        }
    }

    public void Tick()
    {
        double now = AudioSettings.dspTime;
        double elapsed = now - spawnDSPTime;
        float moved = (float)(elapsed * speed);
        transform.position = startPos + Vector3.back * moved;
    }

    public void SetSpawnTime(double dspTime)
    {
        spawnDSPTime = dspTime;
    }
}
