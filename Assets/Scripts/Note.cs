using UnityEngine;

public class Note : MonoBehaviour
{
    public float targetTime;

    void Update()
    {
        transform.Translate(Vector3.back * 5f * Time.deltaTime);
    }
}