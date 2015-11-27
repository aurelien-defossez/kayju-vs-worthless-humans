using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public AudioClip intro, loop;
	public GameObject player;

	private double nextEventTime;
	private AudioSource audioSource;
	private bool paused;

	void Awake () {
		paused = false;
		initAudioSource(intro);
        audioSource.Play();

		StartCoroutine(WaitBeforeLoop());
    }

	IEnumerator WaitBeforeLoop() {
		while (paused || audioSource.isPlaying) {
			yield return null;
		}

		initAudioSource(loop);
		audioSource.loop = true;
		audioSource.PlayScheduled(nextEventTime);
	}

	public void Pause() {
		paused = true;
		audioSource.Pause();
    }

	public void Resume() {
		audioSource.UnPause();
		paused = false;
	}

	private void initAudioSource(AudioClip clip) {
		var child = Instantiate(player);
		child.transform.parent = gameObject.transform;
		audioSource = child.GetComponent<AudioSource>();
		audioSource.clip = clip;
	}
}
