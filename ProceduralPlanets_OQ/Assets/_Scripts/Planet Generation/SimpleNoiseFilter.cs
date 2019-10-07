using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter {

    NoiseSettings.SimpleNoiseSettings settings;
    Noise noise = new Noise();
    private bool sRandomizeSettings = false;

    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        if (!sRandomizeSettings)
        {
            settings.strength = Random.Range(settings.minStrength, settings.maxStrength);
            settings.numLayers = Random.Range(settings.minNumLayers, settings.maxNumLayers);
            settings.baseRoughness = Random.Range(settings.minBaseRoughness, settings.maxBaseRoughness);
            settings.roughness = Random.Range(settings.minRoughness, settings.maxRoughness);
            settings.persistence = Random.Range(settings.minPersistence, settings.maxPersistence);
            settings.minValue = Random.Range(settings.minMinValue, settings.maxMinValue);
            sRandomizeSettings = true;
        }

        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}
