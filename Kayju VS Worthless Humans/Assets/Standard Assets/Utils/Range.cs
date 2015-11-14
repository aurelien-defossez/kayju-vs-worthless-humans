using UnityEngine;
using System;

[System.Serializable]
public class Range {
    public float from;
    public float to;

    public float Lerp(float delta) {
        return Mathf.Lerp(from, to, delta);
    }

    public float InverseLerp(float value) {
        return Mathf.InverseLerp(from, to, value);
    }
    public float Rand() {
        return UnityEngine.Random.Range(from, to);
    }
}

[System.Serializable]
public class IntRange {
    public int from;
    public int to;

    public int Rand() {
        return UnityEngine.Random.Range(from, to + 1);
    }
}