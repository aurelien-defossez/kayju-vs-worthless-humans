﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float initialSpeed;
    float speed;
    public int player;
    Rigidbody2D body;
    string input_x = "Horizontal_J";
    string input_y = "Vertical_J";
    int layerStomp;
    int layerBile;

    // Use this for initialization
    void Start() {
        body = GetComponent<Rigidbody2D>();
        layerStomp = LayerMask.NameToLayer("Stomp");
        layerBile = LayerMask.NameToLayer("Bile");
        speed = initialSpeed;

        if (KaijuDebug.instance.firstPlayerIsHuman) {
            input_x = "Horizontal_Kaiju";
            input_y = "Vertical_Kaiju";
        }
        else {
            input_x += player;
            input_y += player;
        }
    }

    // Update is called once per frame
    void Update() {
        if (player >= 2 && player <= 4)
            body.velocity = new Vector2(Input.GetAxis(input_x), Input.GetAxis(input_y)) * speed;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerStomp)) {
            Die_Human();
        } else if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed / 3;
        }
    }

	void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed / 3;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed;
        }
    }

    void Die_Human(int death = 0) {
        // Death animation goes here, parameter defines which one is played.
        Destroy(gameObject);
    }
}
