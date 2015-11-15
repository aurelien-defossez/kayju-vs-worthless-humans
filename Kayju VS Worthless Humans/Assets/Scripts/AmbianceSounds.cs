using UnityEngine;

public class AmbianceSounds : MonoBehaviour {
	public AudioClip[] ambianceSounds;
	public Range delay;

	float timer;

	public void Start() {
		PlaySound();
	}

	public void Update() {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			PlaySound();
		}
	}

	private void PlaySound() {
		Utils.PlayPitchedClipAt(ambianceSounds[Random.Range(0, ambianceSounds.Length)],
			new Vector3(Random.value < 0.5 ? -10 : 10, 0, 0), Random.Range(0.8f, 1.2f));
		timer = delay.Rand();
	}
}
