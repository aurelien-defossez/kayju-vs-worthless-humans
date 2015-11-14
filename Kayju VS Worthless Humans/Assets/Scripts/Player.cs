using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public int player;
    Rigidbody2D body;
    string input_x = "Horizontal_J";
    string input_y = "Vertical_J";

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();

        if (KaijuDebug.instance.firstPlayerIsHuman) {
            input_x = "Horizontal_Kaiju";
            input_x = "Vertical_Kaiju";
        } else {
            input_x += player;
            input_y += player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player >= 2 && player <= 4)
            body.velocity = new Vector2(Input.GetAxis(input_x), Input.GetAxis(input_y)) * 5;
    }

    void Die_Human(int death = 0)
    {
        // Death animation goes here, parameter defines which one is played.
        Destroy(gameObject);
    }
}
