using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioReader : MonoBehaviour {

    public AudioClip clip;
    [Space]
    public bool changeTime;
    public float audioTime;
    [HideInInspector]
    public float[] audioSamples = new float[512];

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.Play();
    }

    void Update()
    {
        if (changeTime) { audioSource.time = audioTime; changeTime = false; audioTime = 0; }

        audioSource.GetSpectrumData(audioSamples, 0, FFTWindow.Hanning);
    }
}
