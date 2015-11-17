using UnityEngine;

public class BloodStain : MonoBehaviour {
	public AudioClip[] stompDeathSounds;

	public void Start() {
        AudioSource s = GetComponent<AudioSource>();
        s.clip = stompDeathSounds[Random.Range(0, stompDeathSounds.Length)];
        s.pitch = Random.Range(0.8f, 1.2f);
        s.Play();
	}
}
