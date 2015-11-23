using UnityEngine;

public class CursorMove : MonoBehaviour {
	public float speed;

	private Rigidbody2D body;

	void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	public void Update() {
		Vector2 direction = new Vector2(Input.GetAxis("Horizontal_Kaiju"), Input.GetAxis("Vertical_Kaiju"));
		body.velocity = direction * speed;
	}
}
