using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {

    public enum FilterType { Simple, Ridgid };
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float minStrength, maxStrength;
        [HideInInspector]
        public float strength = 1;
        [Range(1, 8)]
        public int minNumLayers, maxNumLayers;
        [HideInInspector]
        public int numLayers = 1;
        public float minBaseRoughness, maxBaseRoughness;
        [HideInInspector]
        public float baseRoughness = 1;
        public float minRoughness, maxRoughness;
        [HideInInspector]
        public float roughness = 2;
        public float minPersistence, maxPersistence;
        [HideInInspector]
        public float persistence = .5f;
        public Vector3 minCentre, maxCentre;
        [HideInInspector]
        public Vector3 centre;
        public float minMinValue, maxMinValue;
        [HideInInspector]
        public float minValue;
    }

    [System.Serializable]
    public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        public float minWeightMultiplier, maxWeightMultiplier;
        [HideInInspector]
        public float weightMultiplier = .8f;
    }
}
