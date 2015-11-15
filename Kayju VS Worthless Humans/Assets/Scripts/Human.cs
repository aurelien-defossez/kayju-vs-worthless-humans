using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {
    public float initialSpeed;
    public Transform scene;
    public Human master;
    public Human slave;

    public Transform BloodStain;
    public Transform Grill;


    public int player;
    bool isPlayer;
    string input_x = "Horizontal_J";
    string input_y = "Vertical_J";

    Animator anim;
    Rigidbody2D body;
    int layerStomp;
    int layerBile;
    int layerLaser;
    int layerPlayer;

    GameObject ScoreBoard;

    protected float speed;
    protected float angle;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        layerStomp = LayerMask.NameToLayer("Stomp");
        layerBile = LayerMask.NameToLayer("Bile");
        layerLaser = LayerMask.NameToLayer("Laser");
        layerPlayer = LayerMask.NameToLayer("Player");
        speed = initialSpeed;
        ScoreBoard = GameObject.Find("GameManager");
        if (player > 0) {
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
            isPlayer = true;
        }
        else {
            isPlayer = false;
        }
    }

    public bool IsPlayer() { return isPlayer; }
    public bool IsPNJ() { return !isPlayer; }

    // To pass the lead
    public void SetPlayer(int player) {
        this.player = player;
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
        isPlayer = true;
    }

    void Update() {
        if (speed < initialSpeed) {
            speed += Time.deltaTime * (initialSpeed / 3);
            speed = Mathf.Min(speed, initialSpeed);
        }
        SetVelocity();
        SetPosture();
    }

    void SetVelocity() {
        if (IsPlayer()) {
            SetVelocityPlayer();
        }
        else if (IsPNJ()) {
            SetVelocityPNJ();
        }
    }

    void SetPosture() {
        if (body.velocity.magnitude > 0) {
            if (angle <= 45.0)
                anim.SetTrigger(Animator.StringToHash("Dos"));
            else if (angle >= 135.0)
                anim.SetTrigger(Animator.StringToHash("Face"));
            else {
                anim.SetTrigger("Coté");

                if (body.velocity.x > 0 && this.transform.localScale.x < 0 || body.velocity.x < 0 && this.transform.localScale.x > 0)
                    this.transform.localScale = Vector3.Scale(this.transform.localScale, new Vector3(-1, 1, 1));
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

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.layer.Equals(layerPlayer)) {
            Debug.Log("player");
            if (IsPNJ()) {
                if (master == null) {
                    Human collidedHuman = collider.gameObject.GetComponent<Human>();
                    if (collidedHuman.IsPlayer()) {
                        master = collidedHuman.SetSlave(this);
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed / 3;
        }
    }

    void Die_Human(int death) {
        if (slave != null) {
            slave.master = master;
            if (IsPlayer()) {
                slave.SetPlayer(player);
            }
        }
        else {
            if (IsPlayer()) {
                ScoreBoard.GetComponent<ScoringBoard>().removePlayer(player);
            }
        }
        if (master != null) {
            master.slave = slave;
        }


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
        corpse.position = this.transform.position.Z(8);


        ScoreBoard.GetComponent<ScoringBoard>().Score_up(0);
        // Death animation goes here, parameter defines which one is played.
        Destroy(gameObject);
    }

    public Human SetSlave(Human human) {
        if (this.slave != null) {
            return this.slave.SetSlave(human);
        }
        else {
            this.slave = human;
            return this;
        }
    }

    void SetVelocityPlayer() {
        Vector2 pos = new Vector2(Input.GetAxis(input_x), Input.GetAxis(input_y)) * speed;
        angle = Vector2.Angle(Vector2.up, pos);

        if (player >= 2 && player <= 4)
            GetComponent<Rigidbody2D>().velocity = pos;
    }

    void SetVelocityPNJ() {
        if (master != null) {
            float deltaX = master.transform.position.x - this.transform.position.x;
            float deltaY = master.transform.position.y - this.transform.position.y;

            Vector2 pos = new Vector2(deltaX, deltaY) * speed;
            angle = Vector2.Angle(Vector2.up, pos);
            if (Mathf.Abs(deltaX) < 0.5 && Mathf.Abs(deltaY) < 0.5) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else { GetComponent<Rigidbody2D>().velocity = pos; }
        }
    }
}
