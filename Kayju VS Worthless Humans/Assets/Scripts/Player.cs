﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public int player;
    Rigidbody2D body;
    string input_y = "Vertical_J";
    string input_x = "Horizontal_J";

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        input_x += player;
        input_y += player;
        body.velocity = new Vector2(Input.GetAxis(input_y), Input.GetAxis(input_x)) * 5;
    }
}
