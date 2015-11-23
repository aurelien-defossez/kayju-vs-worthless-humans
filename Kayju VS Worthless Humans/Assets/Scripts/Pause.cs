using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public Button[] buttons;
	public Button initialButton;

	public void Awake() {
		gameObject.SetActive(false);
	}

	public void OnEnable() {
		foreach (Button button in buttons) {
			button.Deactivate();
		}
		
		Time.timeScale = 0;
		initialButton.Activate();
	}

	public void OnDisable() {
		Time.timeScale = 1;
	}

	public void Update() {
		if (Input.GetButtonUp("Kaiju_Pause") || Input.GetButtonUp("Kaiju_Stomp") || Input.GetButtonUp("Kaiju_Laser")) {
			gameObject.SetActive(false);
		}
	}
}
