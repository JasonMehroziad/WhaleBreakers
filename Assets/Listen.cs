using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listen : MonoBehaviour
{
    public AudioSource listen;

    public static float[] spectrum = new float[512];
    public static float[] simplified = new float[8];
    public static float[] buffer = new float[8];
    private float[] bufferVal = new float[8];

    public UnityEngine.Audio.AudioMixerGroup mic;
    public UnityEngine.Audio.AudioMixerGroup master;

    void Start()
    {
        listen = GetComponent<AudioSource>();
        listen.outputAudioMixerGroup = mic;
        listen.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 10, 44100);
    }   

    void Update()
    {
        int count = 0;

        listen.Play();
        listen.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int limit = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                count += 2;
            }

            for(int j = 0; j < limit; j++)
            {
                average += spectrum[count] * (count + 1);
                count++;
            }

            average /= count;
            simplified[i] = average * 10;
        }

        for (int k = 0; k < 8; k++)
        {
            if(simplified[k] > buffer[k])
            {
                buffer[k] = simplified[k];
                bufferVal[k] = 0.005f;
            }

            if(simplified[k] < buffer[k])
            {
                buffer[k] -= bufferVal[k];
                bufferVal[k] *= 1.2f;
            }
        }

    }
}
