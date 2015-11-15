using UnityEngine;
using System.Collections;
using System;

public class Player : Human {
    public int player;
    string input_x = "Horizontal_J";
    string input_y = "Vertical_J";
    // Use this for initialization
    protected override void Init() {
        #region Kaiju_player
        if (KaijuDebug.instance.firstPlayerIsHuman) {
            input_x = "Horizontal_Kaiju";
            input_y = "Vertical_Kaiju";
        }
        else {
            input_x += player;
            input_y += player;
        }
        #endregion
    }

    protected override void SetVelocity() {
        Vector2 pos = new Vector2(Input.GetAxis(input_x), Input.GetAxis(input_y)) * speed;
        angle = Vector2.Angle(Vector2.up, pos);

        if (player >= 2 && player <= 4)
            GetComponent<Rigidbody2D>().velocity = pos;
    }
}
