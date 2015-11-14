using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class KaijuDebug : MonoBehaviour  {
    // ----------------------------------------------------------------------------
    // Configuration
    // ----------------------------------------------------------------------------

    // Gameplay
    [Tooltip("Debug example")]
    public bool example;

    // ----------------------------------------------------------------------------
    // Singleton
    // ----------------------------------------------------------------------------

    public static KaijuDebug instance { get; private set; }

    public void Awake() {
        instance = this;
    }
}
