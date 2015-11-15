using UnityEngine;
using System.Collections;

public abstract class Human : MonoBehaviour {
    public float initialSpeed;
    public Transform scene;
    public Human master;
    public Human slave;

    public Transform BloodStain;
    public Transform Grill;

    Animator anim;
    Rigidbody2D body;
    int layerStomp;
    int layerBile;
    int layerLaser;

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

        speed = initialSpeed;
        ScoreBoard = GameObject.Find("Score");
        Init();
    }

    protected abstract void Init();

    void Update() {
        if (speed < initialSpeed) {
            speed += Time.deltaTime * (initialSpeed / 3);
            speed = Mathf.Min(speed, initialSpeed);
        }
        SetVelocity();
        SetPosture();
    }

    protected abstract void SetVelocity();

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

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.layer.Equals(layerBile)) {
            speed = initialSpeed / 3;
        }
    }

    void Die_Human(int death) {
        if (master != null) {
            master.slave = null;
        }
        if (slave != null) {
            slave.master = null;
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
}
