using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFallDelayTester : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform spawnPoint;
    public Transform judgeLine;
    public float fallSpeed = 5f;
    public int testCount = 10;

    private List<double> measuredDelays = new List<double>();

    void Start()
    {
        StartCoroutine(RunDSPBasedFallDelayTest());
    }

    IEnumerator RunDSPBasedFallDelayTest()
    {
        for (int i = 0; i < testCount; i++)
        {
            GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
            double spawnDSPTime = AudioSettings.dspTime;
            float startZ = spawnPoint.position.z;
            float endZ = judgeLine.position.z;

            while (note != null)
            {
                double currentDSPTime = AudioSettings.dspTime;
                double elapsed = currentDSPTime - spawnDSPTime;
                float movedDistance = (float)(elapsed * fallSpeed);

                float newZ = startZ - movedDistance;
                Vector3 pos = note.transform.position;
                pos.z = newZ;
                note.transform.position = pos;

                if (newZ <= endZ)
                {
                    double arriveTime = AudioSettings.dspTime;
                    double delay = arriveTime - spawnDSPTime;
                    measuredDelays.Add(delay);

                    Debug.Log($"[DSP TEST {i + 1}] fallDelay = {delay:F3}s");
                    Destroy(note);
                    break;
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        if (measuredDelays.Count > 0)
        {
            double sum = 0;
            foreach (var d in measuredDelays) sum += d;
            double avg = sum / measuredDelays.Count;
            Debug.Log($"[DSP RESULT] Average fallDelay = {avg:F3}s");
        }
    }
}