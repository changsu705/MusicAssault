//using UnityEngine;

//public interface INote
//{
//    float TargetTime { get; }
//    void Initialize(float time, float duration = 0f);
//    void Tick();
//}

//public class ShootNote : MonoBehaviour, INote
//{
//    public float targetTime;
//    public float speed = 5f;

//    public float TargetTime => targetTime;

//    public void Initialize(float time, float duration = 0f)
//    {
//        targetTime = time;
//    }

//    public void Tick()
//    {
//        transform.Translate(Vector3.back * speed * Time.deltaTime);
//    }
//}

//public class LongNote : MonoBehaviour, INote
//{
//    public float targetTime;
//    public float duration;
//    public float speed = 5f;
//    public Transform body;

//    public float TargetTime => targetTime;

//    public void Initialize(float time, float duration = 0f)
//    {
//        targetTime = time;
//        this.duration = duration;

//        float length = speed * duration;
//        body.localScale = new Vector3(1, 1, length);
//        body.localPosition = new Vector3(0, 0, length / 2f);
//    }

//    public void Tick()
//    {
//        transform.Translate(Vector3.back * speed * Time.deltaTime);
//    }
//}
