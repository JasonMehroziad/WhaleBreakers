using UnityEngine;
using System.Collections;

public class GetFrequency : MonoBehaviour
{
    public float sensitivity = 100.0f;
    public float loudness = 0.0f;
    public float frequency = 0.0f;
    public int samplerate = 11024;

    void Start()
    {
        GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, 44100);
        GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
        GetComponent<AudioSource>().mute = true; // Mute the sound, we don't want the player to hear it
        while (!(Microphone.IsRecording(null))) { } // Wait until the recording has started
        GetComponent<AudioSource>().Play(); // Play the audio source!
    }

    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
        frequency = GetFundamentalFrequency();
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        GetComponent<AudioSource>().GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    float GetFundamentalFrequency()
    {
        float fundamentalFrequency = 0.0f;
        float[] data = new float[8192];
        GetComponent<AudioSource>().GetSpectrumData(data, 0, FFTWindow.BlackmanHarris);
        float s = 0.0f;
        int i = 0;
        for (int j = 1; j < 8192; j++)
        {
            if (s < data[j])
            {
                s = data[j];
                i = j;
            }
        }
        fundamentalFrequency = i * samplerate / 8192;
        return fundamentalFrequency;
    }
}