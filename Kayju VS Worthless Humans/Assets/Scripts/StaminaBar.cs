using UnityEngine;
using System.Collections;

public class StaminaBar : MonoBehaviour {
    public float reloadSpeed;
	public float minSize;
	public Transform staminaBar;

	float value;

	// Use this for initialization
	void Start() {
		value = 1;
    }

    // Update is called once per frame
    void Update() {
        if (value < 1f) {
            value += Time.deltaTime * reloadSpeed;
			value = Mathf.Min(value, 1);

			if (staminaBar != null) {
				staminaBar.localScale = Vector2.one * Mathf.Lerp(minSize, 1, value);
			}
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
