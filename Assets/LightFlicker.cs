using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light pointLight;
    float x, y;
    [SerializeField] private float noiseAmplitude;
    [SerializeField] private float noiseFrequency;
    float initialAmplitude;



    void Start()
    {
        pointLight = GetComponent<Light>();
        initialAmplitude = pointLight.intensity;
        x = Random.Range(0, 1000f);
        y = Random.Range(0, 1000f);
    }

    void Update()
    {
        x += Time.deltaTime;
        y += Time.deltaTime*3.1f;
        pointLight.intensity = Mathf.PerlinNoise(x * noiseFrequency, y * noiseFrequency + 32.4f) * noiseAmplitude + initialAmplitude - noiseAmplitude * 0.5f;
    }
}
