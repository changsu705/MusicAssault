using UnityEngine;

public class ShootNote : MonoBehaviour, INote
{
    public float targetTime;
    public float speed = 5f;
    private Vector3 startPos;
    private double spawnDSPTime;
    public int Line { get; private set; }

    public float TargetTime => targetTime;

    public void Initialize(float time, float duration = 0f, int line = 0)
    {
        targetTime = time;
        Line = line;
        startPos = transform.position;
        spawnDSPTime = AudioSettings.dspTime;
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
