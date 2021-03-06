﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class KaijuDebug : MonoBehaviour  {
    // ----------------------------------------------------------------------------
    // Configuration
    // ----------------------------------------------------------------------------

    // Gameplay
    [Tooltip("Forces the first player to be human instead of Kaiju")]
    public bool firstPlayerIsHuman;

	[Tooltip("Never stop hitting them")]
	public bool infiniteStamina;

    // ----------------------------------------------------------------------------
    // Singleton
    // ----------------------------------------------------------------------------

    public static KaijuDebug instance { get; private set; }

    public void Awake() {
        instance = this;
    }
}
