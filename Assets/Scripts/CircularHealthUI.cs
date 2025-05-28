using UnityEngine;

public class CircularHealthUI : MonoBehaviour
{
    public GameObject unitPrefab;  // 체력 바 조각 프리팹
    public int maxHealth = 120;
    public int unitPerSegment = 10;
    private int segmentCount;
    private GameObject[] segments;

    public float radius = 1.0f; // 원의 반지름

    void Start()
    {
        segmentCount = maxHealth / unitPerSegment;
        segments = new GameObject[segmentCount];

        for (int i = 0; i < segmentCount; i++)
        {
            float angle = 360f * i / segmentCount;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * radius;
            Quaternion rot = Quaternion.Euler(0, angle, 0);

            segments[i] = Instantiate(unitPrefab, transform);
            segments[i].transform.localPosition = pos;
            segments[i].transform.localRotation = rot;
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        int activeCount = currentHealth / unitPerSegment;

        for (int i = 0; i < segmentCount; i++)
        {
            segments[i].SetActive(i < activeCount);
        }
    }
}
