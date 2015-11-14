using UnityEngine;
using System.Collections;

public class KaijuCursor : MonoBehaviour {
    Rigidbody2D body;
    public float speed;
    public float laserSpeedModifier;

    public Transform stompPrefab;
    public Transform bilePrefab;
    public Transform laserPrefab;

    public StaminaBar stompStamina;
    public StaminaBar bileStamina;
    public StaminaBar laserStamina;
    public StaminaBar fireStamina;

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
                if (stompStamina.getValue() >= 1f) {
                    Debug.Log("Kaiju_Stomp");
                    Transform stomp = (Transform)Instantiate(stompPrefab);
                    stomp.position = this.transform.position;
                    stompStamina.use(1f);
                }
                else {
                    Debug.Log("You've not enough minerals");
                }
            }

            // LASER!!!
            if (Input.GetButtonDown("Kaiju_Laser") && laserStamina.getValue() > 0.1f) {
                Debug.Log("Kaiju_Laser");
                Transform laserTransform = (Transform)Instantiate(laserPrefab);
                laser = (Laser)laserTransform.GetComponent<Laser>();
            }

            if (laser != null) {
                laser.setPosition(this.transform.position);
                laserStamina.use(Time.deltaTime * 0.3f);
                if (Input.GetButtonUp("Kaiju_Laser") || laserStamina.getValue() <= 0) {
                    laser.Kill();
                    laser = null;
                }
            }



            // EEXCCUUUUUSSE MEEEE!
            if (Input.GetButtonDown("Kaiju_Bile")) {
                if (bileStamina.getValue() > 0.5f) {
                    Debug.Log("Kaiju_Bile");
                    Transform bile = (Transform)Instantiate(bilePrefab);
                    bile.position = this.transform.position;
                    bileStamina.use(0.5f);
                }
                else {
                    Debug.Log("You require more vespene gaz");
                }
            }

            // KAIJU FIRE!
            if (Input.GetButtonDown("Kaiju_Fire") && fireStamina.getValue() > 0.1f) {
                Debug.Log("Kaiju_Fire");
            }

            Vector2 direction = new Vector2(Input.GetAxis("Horizontal_Kaiju"), Input.GetAxis("Vertical_Kaiju"));
            body.velocity = direction * speed;

            if (laser != null) {
                body.velocity *= laserSpeedModifier;
            }
        }
    }
}
