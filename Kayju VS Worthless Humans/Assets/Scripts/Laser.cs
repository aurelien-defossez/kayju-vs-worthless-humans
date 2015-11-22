using UnityEngine;

public class Laser : MonoBehaviour {
	public Transform laser;
	public Vector3 origin;
	public Transform laserTracePrefab;
	public Transform scene;

	public void setPosition(Vector2 position) {
		transform.position = new Vector3(position.x, position.y, 0);

		Transform trace = (Transform)Instantiate(laserTracePrefab);
        trace.position = transform.position;
		trace.rotation = Quaternion.Euler(0, 0, Random.value * 360);
		trace.parent = scene;

		Rotate();
	}

	private void Rotate() {
		float angle = Vector2.Angle(origin - transform.position, origin);
		if (transform.position.x > 0) {
			angle *= -1;
		}

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	public void Kill() {
		Destroy(gameObject);
	}
}
