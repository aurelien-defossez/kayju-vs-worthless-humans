using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {
	public Button previousButton;
	public Button nextButton;
	public SpriteRenderer activeSprite;
	public Text text;
	public Color passiveColor;
	public Color activeColor;
	public float sensitivity;

	private bool active;
	private bool isNeutral;

	public void Activate() {
		text.color = activeColor;
		activeSprite.enabled = true;
		active = true;
		isNeutral = false;

		GetComponentInChildren<AudioSource>().Play();
	}

	public void Deactivate() {
		text.color = passiveColor;
		activeSprite.enabled = false;
		active = false;
    }

	public void Update() {
		if (active) {
			float dx = Input.GetAxis("Horizontal_Kaiju");

			if (isNeutral) {
				if (Input.GetButtonUp("Kaiju_Bile")) {
					GetComponent<ButtonEffect>().doEffect();
				} else if (previousButton != null && dx < -sensitivity) {
					Deactivate();
					previousButton.Activate();
				} else if (nextButton != null && dx > sensitivity) {
					Deactivate();
					nextButton.Activate();
				}
			} else if (dx == 0) {
				isNeutral = true;
			}
		}
	}
}
