using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public Button[] buttons;
	public Button initialButton;
	public MusicManager musicManager;

	public void Awake() {
		gameObject.SetActive(false);
	}

	public void OnEnable() {
		musicManager.Pause();

		foreach (Button button in buttons) {
			button.Deactivate();
		}
		
		Time.timeScale = 0;
		initialButton.Activate();
	}

	public void OnDisable() {
		musicManager.Resume();
        Time.timeScale = 1;
	}

	public void Update() {
		if (Input.GetButtonUp("Kaiju_Pause") || Input.GetButtonUp("Kaiju_Stomp") || Input.GetButtonUp("Kaiju_Laser")) {
			gameObject.SetActive(false);
		}
	}
}
