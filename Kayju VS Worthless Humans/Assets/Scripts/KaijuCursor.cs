﻿using UnityEngine;
using System.Collections;

public class KaijuCursor : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!KaijuDebug.instance.firstPlayerIsHuman) {
            if (Input.GetButtonDown("Kaiju_Stomp")) {
                Debug.Log("Kaiju_Stomp");
            }
            if (Input.GetButtonDown("Kaiju_Laser")) {
                Debug.Log("Kaiju_Laser");
            }
            if (Input.GetButtonDown("Kaiju_Bile")) {
                Debug.Log("Kaiju_Bile");
            }
            if (Input.GetButtonDown("Kaiju_Fire")) {
                Debug.Log("Kaiju_Fire");
            }

            body.velocity = (new Vector2(Input.GetAxis("Horizontal_Kaiju"), Input.GetAxis("Vertical_Kaiju")) * speed);
        }
    }
}
