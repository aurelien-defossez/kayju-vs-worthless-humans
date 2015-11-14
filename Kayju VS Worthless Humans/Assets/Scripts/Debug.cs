using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Debug : MonoBehaviour  {
    // ----------------------------------------------------------------------------
    // Configuration
    // ----------------------------------------------------------------------------

    // Gameplay
    [Tooltip("Debug example")]
    public bool example;

    // ----------------------------------------------------------------------------
    // Singleton
    // ----------------------------------------------------------------------------

    public static Debug instance { get; private set; }

    public void Awake() {
        instance = this;
    }
}
