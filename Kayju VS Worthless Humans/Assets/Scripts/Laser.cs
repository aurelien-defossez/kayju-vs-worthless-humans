using UnityEngine;

public class Laser : MonoBehaviour {
	public Transform laser;
	public Vector2 origin;

	public void setPosition(Vector2 position) {
		transform.position = position;

		float angle = Vector2.Angle(origin - position, origin);
		if (position.x > 0) {
			angle *= -1;
		}

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
