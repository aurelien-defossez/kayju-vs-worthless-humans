using UnityEngine;

public class Laser : MonoBehaviour {
	public Transform laser;
	public Vector3 origin;

	public void setPosition(Vector2 position) {
		transform.position = new Vector3(position.x, position.y, 0);
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
