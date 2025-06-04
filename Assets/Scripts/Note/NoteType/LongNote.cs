using UnityEngine;

public class LongNote : MonoBehaviour, INote
{
    public float targetTime;
    public float duration;
    public float speed = 5f;
    public Transform body;

    public float TargetTime => targetTime;
    private bool isHolding = false;
    private float holdStartTime;

    public void Initialize(float time, float duration = 0f)
    {
        targetTime = time;
        this.duration = duration;

        float length = speed * duration;
        body.localScale = new Vector3(1, 1, length);
        body.localPosition = new Vector3(0, 0, length / 2f);
    }

    public void StartHold()
    {
        isHolding = true;
        holdStartTime = Time.time;
    }

    public void EndHold()
    {
        isHolding = false;
        Destroy(gameObject);
    }

    public void Tick()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (isHolding)
        {
            float elapsed = Time.time - holdStartTime;
            float remain = Mathf.Max(0f, duration - elapsed);
            float length = speed * remain;
            body.localScale = new Vector3(1, 1, length);
            body.localPosition = new Vector3(0, 0, length / 2f);
        }
    }
}
