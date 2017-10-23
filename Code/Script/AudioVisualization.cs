using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioVisualization : MonoBehaviour
{
    public AudioReader audioReader;

    public enum AudioVisualization_Type { none = 0, linear, circle, tunnel }
    public enum ParticleEffect_Type { none = 0, play, lifeTime, startSpeed }

    public AudioVisualization_Type _AudioVisualizationType = AudioVisualization_Type.none;
    public ParticleEffect_Type _ParticleEffectType = ParticleEffect_Type.none;

    [Space]
    public bool rotation;
    public Vector2 rotateSensitivityRange;
    public float rotateSensitivity;
    public Vector3 rotateAmmount;
    [Space]
    public bool scale;
    public Vector2 scaleSensitivityRange;
    public float scaleSensitivity;
    public float scaleAmmount;
    public float scaleSmooth;
    [Space]
    public bool color;
    public Color minColor;
    public Color maxColor;
    public Color inverted_minColor;
    public Color inverted_maxColor;
    public float maxColorAmmount;
    public float mulColorAmmount;
    public float inverted_maxColorAmmount;
    public float inverted_mulColorAmmount;
    [Space]
    public bool movement;
    public float movXAmmount;
    public float movXSpeed;
    public float movYAmmount;
    public float movYSpeed;
    [Space]

    public GameObject ps_Visualization;
    public Transform parent_Visualization;
    public float objectOffSet = 0.05f;

    [Space]

    public bool centered;

    [Space]

    public Color particleColor;
    public float particleColorIntensity;
    public float audioSensibility = 0.05f;
    public float audioMultiplier = 200f;
    public float audioMax = 200f;
    public float audioSmooth = 0.9f;

    [Space]

    public bool duplicateInverted;
    public Color inverted_particleColor;
    public float inverted_particleColorIntensity;
    public float inverted_audioSensibility = 0.05f;
    public float inverted_audioMultiplier = 200f;
    public float inverted_audioMax = 200f;
    public float inverted_audioSmooth = 0.9f;

    [HideInInspector]
    private ParticleSystem[] particles;
    private ParticleSystem[] inverted_particles;


    private Vector3 originalPosition;

    private void Awake()
    {

        particles = new ParticleSystem[audioReader.audioSamples.Length];
        inverted_particles = new ParticleSystem[audioReader.audioSamples.Length];

        if (movement) { originalPosition = transform.position; }

        for (int i = 0; i < audioReader.audioSamples.Length; i++)
        {
            float i2 = i;

            if (centered)
            {
                if (i % 2 == 0) { i2 = -i2; }
            }

            Vector3 pos = Vector3.zero;

            switch (_AudioVisualizationType)
            {
                case AudioVisualization_Type.linear:
                    pos = transform.position + transform.right * i2 * objectOffSet;
                    particles[i] = Instantiate(ps_Visualization, pos, transform.rotation, parent_Visualization).GetComponent<ParticleSystem>();
                    particles[i].name = "Particle: " + i;
                    if (duplicateInverted)
                    {
                        inverted_particles[i] = Instantiate(ps_Visualization, pos, Quaternion.Inverse(transform.rotation), parent_Visualization).GetComponent<ParticleSystem>();
                        inverted_particles[i].name = "Inverted Particle: " + i;
                    }
                    break;

                case AudioVisualization_Type.circle:
                    pos = new Vector3((Mathf.Sin((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, (Mathf.Cos((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, transform.position.z);
                    particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(pos - transform.position), parent_Visualization).GetComponent<ParticleSystem>();
                    particles[i].name = "Particle: " + i;
                    if (duplicateInverted)
                    {
                        inverted_particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(transform.position - pos), parent_Visualization).GetComponent<ParticleSystem>();
                        inverted_particles[i].name = "Inverted Particle: " + i;
                    }
                    break;
                case AudioVisualization_Type.tunnel:
                    pos = new Vector3((Mathf.Sin((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, (Mathf.Cos((i2 / audioReader.audioSamples.Length) * Mathf.PI)) * objectOffSet, transform.position.z);
                    particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(-Camera.main.transform.forward), parent_Visualization).GetComponent<ParticleSystem>();
                    particles[i].name = "Particle: " + i;
                    if (duplicateInverted)
                    {
                        inverted_particles[i] = Instantiate(ps_Visualization, pos, Quaternion.LookRotation(Camera.main.transform.forward), parent_Visualization).GetComponent<ParticleSystem>();
                        inverted_particles[i].name = "Inverted Particle: " + i;
                    }
                    break;
            }

            var main = particles[i].main;
            main.startColor = particleColor;

            if (duplicateInverted)
            {
                var invmain = inverted_particles[i].main;
                invmain.startColor = inverted_particleColor;
            }
        }

    }

    private void LateUpdate()
    {


        switch (_ParticleEffectType)
        {
            case ParticleEffect_Type.lifeTime:
                for (int i = 0; i < audioReader.audioSamples.Length; i++)
                {
                    if (audioReader.audioSamples[i] * audioMultiplier > audioMax) { audioReader.audioSamples[i] = audioMax / audioMultiplier; }
                    if (audioReader.audioSamples[i] > audioSensibility)
                    {
                        var main = particles[i].main;
                        main.startLifetime = Mathf.Lerp(main.startLifetime.constant, audioReader.audioSamples[i] * audioMultiplier, audioSmooth);
                    }
                    else
                    {
                        var main = particles[i].main;
                        main.startLifetime = 0f;
                    }

                    if (duplicateInverted)
                    {
                        if (audioReader.audioSamples[i] * inverted_audioMultiplier > inverted_audioMax) { audioReader.audioSamples[i] = inverted_audioMax / inverted_audioMultiplier; }
                        if (audioReader.audioSamples[i] > inverted_audioSensibility)
                        {
                            var main = inverted_particles[i].main;
                            main.startLifetime = Mathf.Lerp(main.startLifetime.constant, audioReader.audioSamples[i] * inverted_audioMultiplier, inverted_audioSmooth);
                        }
                        else
                        {
                            var main = inverted_particles[i].main;
                            main.startLifetime = 0f;
                        }
                    }
                }
                break;
            case ParticleEffect_Type.startSpeed:
                for (int i = 0; i < audioReader.audioSamples.Length; i++)
                {
                    if (audioReader.audioSamples[i] * audioMultiplier > audioMax) { audioReader.audioSamples[i] = audioMax / audioMultiplier; }
                    if (audioReader.audioSamples[i] > audioSensibility)
                    {
                        var main = particles[i].main;
                        main.startSpeed = Mathf.Lerp(main.startSpeed.constant, audioReader.audioSamples[i] * audioMultiplier, audioSmooth);
                    }
                    else
                    {
                        var main = particles[i].main;
                        main.startSpeed = 0f;
                        if (duplicateInverted)
                        {
                            var inv_main = inverted_particles[i].main;
                            inv_main.startSpeed = 0;
                        }
                    }

                    if (duplicateInverted)
                    {
                        if (audioReader.audioSamples[i] * inverted_audioMultiplier > inverted_audioMax) { audioReader.audioSamples[i] = inverted_audioMax / inverted_audioMultiplier; }
                        if (audioReader.audioSamples[i] > inverted_audioSensibility)
                        {
                            var main = inverted_particles[i].main;
                            main.startSpeed = Mathf.Lerp(main.startSpeed.constant, audioReader.audioSamples[i] * inverted_audioMultiplier, inverted_audioSmooth);
                        }
                        else
                        {
                            var main = inverted_particles[i].main;

                        }
                    }
                }
                break;

            case ParticleEffect_Type.play:
                for (int i = 0; i < audioReader.audioSamples.Length; i++)
                {
                    if (audioReader.audioSamples[i] > audioSensibility)
                    {
                        if (particles[i].IsAlive())
                        {
                            particles[i].Play();
                        }
                    }

                    if (duplicateInverted)
                    {
                        if (audioReader.audioSamples[i] > inverted_audioSensibility)
                        {
                            if (inverted_particles[i].IsAlive())
                            {
                                inverted_particles[i].Play();
                            }
                        }
                    }
                }
                break;
        }

        if (rotation)
        {
            float sum = 0f;

            for (int i = (int)rotateSensitivityRange.x; i < rotateSensitivityRange.y; i++)
            {
                if (audioReader.audioSamples[i] > rotateSensitivity)
                {
                    sum += audioReader.audioSamples[i];
                }
            }

            float average = sum / (rotateSensitivityRange.y - rotateSensitivityRange.x);

            parent_Visualization.Rotate(rotateAmmount * average,Space.Self);
        }
        if (scale)
        {
            float sum = 0f;

            for (int i = (int)scaleSensitivityRange.x; i < scaleSensitivityRange.y; i++)
            {
                if (audioReader.audioSamples[i] > scaleSensitivity)
                {
                    sum += audioReader.audioSamples[i];
                }
            }

            float average = sum / (scaleSensitivityRange.y - scaleSensitivityRange.x);

            parent_Visualization.localScale = Vector3.Lerp(parent_Visualization.localScale, new Vector3(1 + (average * scaleAmmount), 1 + (average * scaleAmmount), 1 + (average * scaleAmmount)), scaleSmooth);
        }
        if (color)
        {
            for (int i = 0; i < audioReader.audioSamples.Length; i++)
            {
                float k = audioReader.audioSamples[i] * mulColorAmmount / maxColorAmmount;

                var main = particles[i].main;
                main.startColor = Color.Lerp(minColor, maxColor, k);

                if (duplicateInverted)
                {
                    float k2 = audioReader.audioSamples[i] * inverted_mulColorAmmount / inverted_maxColorAmmount;

                    var invmain = inverted_particles[i].main;
                    invmain.startColor = Color.Lerp(inverted_minColor, inverted_maxColor, k2);
                }
            }
        }
        if (movement)
        {
            parent_Visualization.transform.position = originalPosition + new Vector3(Mathf.Sin(Time.time * movXSpeed)*movXAmmount, Mathf.Cos(Time.time * movYSpeed) * movYAmmount, 0);
        }
    }
}


