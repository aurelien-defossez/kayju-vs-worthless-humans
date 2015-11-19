using UnityEngine;
using System.Collections;

public class Foot : MonoBehaviour {
	public float yLimit;
	public Timeline timeline;
	public AudioClip footSound;
	public Collider2D footCollider;

	Vector2 savedPosition;

	// Update is called once per frame
	void Update() {
		if (transform.position.y < yLimit) {
			savedPosition = transform.position;
            timeline.Restart();	
            timeline.Play();
		}
	}

	[TimelineMethod]
	public void MoveFoot(TimelineCall options) {
		transform.position = savedPosition + new Vector2(0, -yLimit) * options.progress;
	}

	[TimelineMethod]
	public void PlaySound(TimelineCall options) {
        //Utils.PlayPitchedClipAt(footSound, gameObject, Random.Range(0.8f, 1.2f));
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        GetComponent<AudioSource>().Play();
	}

	[TimelineMethod]
	public void Hit(TimelineCall options) {
		footCollider.enabled = true;
	}

	[TimelineMethod]
	public void StopHit(TimelineCall options) {
		footCollider.enabled = false;
	}
}
