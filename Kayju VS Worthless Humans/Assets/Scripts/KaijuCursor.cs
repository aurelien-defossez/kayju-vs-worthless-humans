using UnityEngine;
using System.Collections;

public class KaijuCursor : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    public Transform stompPrefab;
    public Transform bilePrefab;
	public Transform laserPrefab;
	
	Laser laser;

	// Use this for initialization
	void Start() {
        body = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update() {
		if (!KaijuDebug.instance.firstPlayerIsHuman) {
			// BOOM
			if (Input.GetButtonDown("Kaiju_Stomp")) {
				Debug.Log("Kaiju_Stomp");
				Transform stomp = (Transform)Instantiate(stompPrefab);
				stomp.position = this.transform.position;
			}

			// LASER!!!
			if (Input.GetButtonDown("Kaiju_Laser")) {
				Debug.Log("Kaiju_Laser");
				Transform laserTransform = (Transform)Instantiate(laserPrefab);
				laser = (Laser)laserTransform.GetComponent<Laser>();
			}

			if (laser != null) {
				laser.setPosition(this.transform.position);
			}

			if (Input.GetButtonUp("Kaiju_Laser")) {
				laser.Kill();
				laser = null;
			}

			// EEXCCUUUUUSSE MEEEE!
			if (Input.GetButtonDown("Kaiju_Bile")) {
				Debug.Log("Kaiju_Bile");
                Transform bile = (Transform)Instantiate(bilePrefab);
                bile.position = this.transform.position;
            }

			// KAIJU FIRE!
			if (Input.GetButtonDown("Kaiju_Fire")) {
				Debug.Log("Kaiju_Fire");
			}

			body.velocity = (new Vector2(Input.GetAxis("Horizontal_Kaiju"), Input.GetAxis("Vertical_Kaiju")) * speed);
        }
	}
}
