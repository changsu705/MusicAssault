using UnityEngine;
public class AudioCube : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    GameObject[] sampleCube = new GameObject[128];         //샘플 큐브 배열
    public float maxScale = 1000;                           //큐브의 최대 크기
    public float initialXRotation = 90f;           // 처음에 X축으로 눕힐 각도
    public float yRotationSpeed = 30f;             // 지속 회전 속도 (Y축)
    public Vector3 HaloScale = new Vector3(0.3f, 0.3f, 0.3f);
    public Vector3 HaloAngle = new Vector3(-15f, -180f, 0f);

    void Start()
    {
        float radius = 25f;

        for (int i = 0; i < sampleCube.Length; i++)
        {
            GameObject temp = Instantiate(sampleCubePrefab);
            temp.name = "Cube" + i.ToString("000");
            temp.transform.parent = this.transform;

            // 원형 배치
            float angle = i * (360f / sampleCube.Length) + 30f;
            Vector3 pos = Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;
            temp.transform.localPosition = pos;

            // 가운데를 바라보게 회전
            temp.transform.LookAt(this.transform.position);

            sampleCube[i] = temp;
        }

        transform.rotation = Quaternion.Euler(initialXRotation, 0f, 0f);
        transform.localScale = HaloScale;
    }


    void Update()
{

    // Y축으로만 회전
    transform.Rotate(Vector3.up * yRotationSpeed * Time.deltaTime, Space.Self);

    for (int i = 0; i < sampleCube.Length; i++)
    {
        if (sampleCube[i] != null)
        {
            sampleCube[i].transform.localScale
                = new Vector3(10, 10, (AudioPeer.samples[i] * maxScale) + 10) * 0.1f;
        }
    }
}

}
