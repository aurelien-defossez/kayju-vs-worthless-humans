﻿using UnityEngine;
using System.Collections;

public class KaijuCursor : MonoBehaviour {
    Rigidbody2D body;
    public float speed;
    public float laserSpeedModifier;

    public Transform scene;
	public GameObject pauseMenu;

    public Transform stompPrefab;
    public Transform bilePrefab;
    public Transform laserPrefab;
	public Transform firePrefab;

    public StaminaBar stompStamina;
    public StaminaBar bileStamina;
    public StaminaBar laserStamina;
    public StaminaBar fireStamina;

	public float stompStaminaUsage;
    public float bileStaminaUsage;
    public float laserStaminaUsage;
    public float fireStaminaUsage;

	public AudioSource laserStarting;
	public AudioSource laserKilling;

	public float laserRandomForce;
	public float laserRandomRotation;

	public float fireSpitInterval;
    public FireManager fireManager;

	bool spittingFire;
	float fireTimer;
	Laser laser;
	Vector2 randomLaserVelocity;
    float laserOriginalVolume;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody2D>();
        laserOriginalVolume = laserKilling.volume;
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale > 0 && !KaijuDebug.instance.firstPlayerIsHuman) {
			// STOP THIS SHIT!
			if (Input.GetButtonUp("Kaiju_Pause")) {
				pauseMenu.SetActive(true);
			}

            // BOOM
            if (Input.GetButtonDown("Kaiju_Stomp")) {
                if (stompStamina.getValue() >= 1f) {
                    Debug.Log("Kaiju_Stomp");
                    Transform stomp = (Transform)Instantiate(stompPrefab);
                    stomp.position = this.transform.position;
					stomp.GetComponent<Stomp>().Init(scene);
                    stompStamina.use(stompStaminaUsage);
                }
                else {
                    Debug.Log("You've not enough minerals");
                }
            }

            // LASER!!!
            if (Input.GetButtonDown("Kaiju_Laser") && laserStamina.getValue() >= laserStaminaUsage * 0.25f) {
                Debug.Log("Kaiju_Laser");
                Transform laserTransform = (Transform)Instantiate(laserPrefab);
				laserTransform.position = this.transform.position;
				laser = (Laser)laserTransform.GetComponent<Laser>();
				laser.scene = scene;
                randomLaserVelocity = Quaternion.Euler(0, 0, Random.value * 360) * Vector2.up * laserRandomForce;
                laserKilling.volume = laserOriginalVolume;
                laserKilling.Play();
            }

            if (laser != null) {
                laser.setPosition(this.transform.position);
                laserStamina.use(Time.deltaTime * laserStaminaUsage);

				if (Input.GetButtonUp("Kaiju_Laser") || laserStamina.getValue() <= 0) {
                    laser.Kill();
					randomLaserVelocity = Vector2.zero;
                    Utils.FadeAudio(laserKilling, 0, 0.25f);
					laser = null;
                } else {
					randomLaserVelocity = randomLaserVelocity.Rotate(laserRandomRotation * Time.deltaTime);
                }
            }

            // EEXCCUUUUUSSE MEEEE!
            if (Input.GetButtonDown("Kaiju_Bile")) {
                if (bileStamina.getValue() > 0.5f) {
                    Debug.Log("Kaiju_Bile");
                    Transform bile = (Transform)Instantiate(bilePrefab);
                    bile.position = this.transform.position;
                    bile.SetParent(scene);
                    bileStamina.use(bileStaminaUsage);
                }
                else {
                    Debug.Log("You require more vespene gaz");
                }
            }

            // KAIJU FAYA ATTAKU DESU!
            if (Input.GetButtonDown("Kaiju_Fire") && fireStamina.getValue() >= fireStaminaUsage) {
				Debug.Log("Kaiju_Fire");
				spittingFire = true;
				fireTimer = 0;
            }

			if (spittingFire) {
				if (fireTimer <= 0) {
					Transform fireTransform = (Transform)Instantiate(firePrefab);
					Fire fire = (Fire)fireTransform.GetComponent<Fire>();
					fire.Init(scene, fireManager, transform.position);

					fireStamina.use(fireStaminaUsage);
					fireTimer += fireSpitInterval;
				}

				fireTimer -= Time.deltaTime;

				if (Input.GetButtonUp("Kaiju_Fire") || fireStamina.getValue() < fireStaminaUsage) {
					spittingFire = false;
				}
			}

			Vector2 direction = new Vector2(Input.GetAxis("Horizontal_Kaiju"), Input.GetAxis("Vertical_Kaiju"));
            body.velocity = direction * speed;
			body.velocity += randomLaserVelocity * Time.deltaTime;

			if (laser != null) {
                body.velocity *= laserSpeedModifier;
            }
        }
    }
}
