using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //오디오 소스 컴포넌트가 반드시 필요하다.
public class AudioPeer : MonoBehaviour
{
    AudioSource audioSource;

    public static float[] samples = new float[512];         //FFT 로 얻은 스팩티럼 데이터 샘플
    public static float[] freqBand = new float[8];          //주파수 대역
    public static float[] bandBuffet = new float[8];        //주파스 대역 버퍼
    float[] bufferDecreas = new float[8];                   //버퍼 감소 속도

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();   //오디오 스펙트럼 데이터를 가져온다.
        MakeFrequenyBand();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequenyBand()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBand[i] = average * 10;
        }
    }

    public void BandBuffer()
{
    for (int i = 0; i < 8; i++)
    {
        if (freqBand[i] > bandBuffet[i])
        {
            bandBuffet[i] = freqBand[i];
            bufferDecreas[i] = 0.005f;
        }
        else if (freqBand[i] < bandBuffet[i])
        {
            bandBuffet[i] -= bufferDecreas[i];
            bufferDecreas[i] *= 1.2f;
        }
    }
}

}
