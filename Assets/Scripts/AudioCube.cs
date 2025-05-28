using UnityEngine;

public class AudioCube : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    GameObject[] sampleCube = new GameObject[512];         //���� ť�� �迭
    public float maxScale = 1000;                           //ť���� �ִ� ũ��
    public float initialXRotation = 90f;           // ó���� X������ ���� ����
public float yRotationSpeed = 30f;             // ���� ȸ�� �ӵ� (Y��)

    void Start()
    {
        float radius = 25f;

        for (int i = 0; i < sampleCube.Length; i++)
        {
            GameObject temp = Instantiate(sampleCubePrefab);
            temp.name = "Cube" + i.ToString("000");
            temp.transform.parent = this.transform;

            // ���� ��ġ
            float angle = i * (360f / sampleCube.Length);
            Vector3 pos = Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;
            temp.transform.localPosition = pos;

            // ����� �ٶ󺸰� ȸ��
            temp.transform.LookAt(this.transform.position);

            sampleCube[i] = temp;
        }

        transform.rotation = Quaternion.Euler(initialXRotation, 0f, 0f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }


    void Update()
{

    // Y�����θ� ȸ��
    transform.Rotate(Vector3.up * yRotationSpeed * Time.deltaTime, Space.Self);

    for (int i = 0; i < sampleCube.Length; i++)
    {
        if (sampleCube[i] != null)
        {
            sampleCube[i].transform.localScale
                = new Vector3(10, (AudioPeer.samples[i] * maxScale) + 2, 10) * 0.1f;
        }
    }
}

}
