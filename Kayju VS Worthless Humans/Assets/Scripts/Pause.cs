using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public Button initialButton;

	public void Awake() {
		gameObject.SetActive(false);
	}

	public void OnEnable() {
		Time.timeScale = 0;
		initialButton.Activate();
	}

	public void OnDisable() {
		Time.timeScale = 1;
	}
}
