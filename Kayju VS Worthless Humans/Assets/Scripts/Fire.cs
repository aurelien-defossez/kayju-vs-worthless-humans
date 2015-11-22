using UnityEngine;

public class Fire : MonoBehaviour {
	public Vector3 spitSpawn;
	public Timeline timeline;
	public GameObject spitFire;
	public GameObject burningFire;

	Transform scene;
	Vector3 target;

	public void Init(Transform scene, Vector3 target) {
		this.scene = scene;
		this.target = target;

		float angle = Vector2.Angle(spitSpawn - this.target, spitSpawn);
		if (target.x > 0) {
			angle *= -1;
		}

		transform.position = spitSpawn;
		spitFire.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		burningFire.SetActive(false);

		timeline.Play();
	}

	[TimelineMethod]
	public void SpitFire(TimelineCall options) {
		transform.position = Vector3.Lerp(spitSpawn, target, options.progress);
	}

	[TimelineMethod]
	public void TouchGround(TimelineCall options) {
		transform.parent = scene;
		spitFire.SetActive(false);
		burningFire.SetActive(true);
	}

	[TimelineMethod]
	public void EndFire(TimelineCall options) {
		burningFire.GetComponentInChildren<Animator>().SetTrigger(Animator.StringToHash("stop"));
	}

	[TimelineMethod]
	public void Kill(TimelineCall options) {
		Destroy(gameObject);
	}
}