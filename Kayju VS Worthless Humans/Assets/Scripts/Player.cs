using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float initialSpeed;
    public Transform scene;

    Transform obj;
    Animator anim;
    Rigidbody2D body;
    public float speed;
    public int player;
    string input_x = "Horizontal_J";
    string input_y = "Vertical_J";
    int layerStomp;
    int layerBile;
    int layerLaser;
    public Transform BloodStain;
    public Transform Grill;
    GameObject ScoreBoard;



    public float angle;
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        obj = GetComponent<Transform>();
        layerStomp = LayerMask.NameToLayer("Stomp");
        layerBile = LayerMask.NameToLayer("Bile");
        layerLaser = LayerMask.NameToLayer("Laser");
        speed = initialSpeed;
        ScoreBoard = GameObject.Find("Score");

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

    // Update is called once per frame
    void Update() {
        if (speed < initialSpeed) {
            speed += Time.deltaTime * (initialSpeed/3);
            speed = Mathf.Min(speed, initialSpeed);
        }

        Vector2 pos = new Vector2(Input.GetAxis(input_x), Input.GetAxis(input_y)) * speed;
        angle = Vector2.Angle(Vector2.up, pos);

        if (player >= 2 && player <= 4)
            body.velocity = pos;

        if (pos.magnitude > 0) {
            if (angle <= 45.0)
                anim.SetTrigger(Animator.StringToHash("Dos"));
            else if (angle >= 135.0)
                anim.SetTrigger(Animator.StringToHash("Face"));
            else {
                anim.SetTrigger("Coté");

                if (pos.x > 0 && obj.localScale.x < 0 || pos.x < 0 && obj.localScale.x > 0)
                   obj.localScale = Vector3.Scale(obj.localScale, new Vector3(-1, 1, 1));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerStomp)) {
            Die_Human(1);
        }
        else if (collider.gameObject.layer.Equals(layerLaser)) {
            Die_Human(2);
        }
        else if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed / 3;
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed / 3;
        }
    }
    
    void Die_Human(int death) {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        Transform corpse;

        rend.enabled = false;
        if (death == 1) {
            corpse = (Transform)Instantiate(BloodStain);
        }
        else { // (death == 2) {
            corpse = (Transform)Instantiate(Grill);
        }
        corpse.SetParent(scene);
        corpse.position = this.transform.position.Z(6);
        ScoreBoard.GetComponent<ScoringBoard>().score["Kaiju"] += 1;
        // Death animation goes here, parameter defines which one is played.
        Destroy(gameObject);
    }
}
