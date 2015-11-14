using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {
    public float reloadSpeed;
    public float value;

    // Use this for initialization
    void Start() {
        value = 0;
    }

    // Update is called once per frame
    void Update() {
        if (value < 1f) {
            value += Mathf.Min(1f, Time.deltaTime * reloadSpeed);
        }
    }

    public void use(float qty) {
        value = Mathf.Max(0, value - qty);
    }

    public float getValue() {
        return value;
    }
}
