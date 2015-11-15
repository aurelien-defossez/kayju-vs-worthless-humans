using UnityEngine;

public class Title : MonoBehaviour {
	public void Update() {
		if (Input.GetButtonDown("Kaiju_Bile")) {
			Application.LoadLevel("game");
		}
	}
}
