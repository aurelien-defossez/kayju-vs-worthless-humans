﻿using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {
    public float reloadSpeed;
    public float value;

    // Use this for initialization
    void Start() {
        value = KaijuDebug.instance.infiniteStamina ? 1 : 0;
    }

    // Update is called once per frame
    void Update() {
        if (value < 1f) {
            value += Time.deltaTime * reloadSpeed;
			value = Mathf.Min(value, 1);
        }
    }

    public void use(float qty) {
		if (!KaijuDebug.instance.infiniteStamina) {
			value = Mathf.Max(0, value - qty);
		}
    }

    public float getValue() {
        return value;
    }
}