using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listen : MonoBehaviour
{
    public AudioSource listen;
    public static float[] spectrum = new float[64];
    public UnityEngine.Audio.AudioMixerGroup mic;
    public UnityEngine.Audio.AudioMixerGroup master;

    void Start()
    {
        listen = GetComponent<AudioSource>();
        listen.outputAudioMixerGroup = mic;
        listen.clip = Microphone.Start(Microphone.devices[0].ToString(), false, 10, 44100);
    }

    void Update()
    {
        listen.Play();
        listen.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
    }
}
