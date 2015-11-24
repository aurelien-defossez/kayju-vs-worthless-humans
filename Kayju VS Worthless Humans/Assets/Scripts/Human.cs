using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {
    public float initialSpeed;
    public Human master;
    public Human slave;
    public Transform BloodStain;
    public Transform Grill;

    public int team;
    public int joystickID;
    public bool isPlayer = false;
    public string input_x;
    public string input_y;
	public float pointsPerHumanPerSecond;

	GameManager gameManager;
    Animator anim;
    Rigidbody2D body;
    int layerStomp;
    int layerBile;
    int layerLaser;
    int layerPlayer;
	Score humanScore;
	float score;

	protected float speed;
    protected float angle;
    
    void Awake() {
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Back");
        body = GetComponent<Rigidbody2D>();
        layerStomp = LayerMask.NameToLayer("Stomp");
        layerBile = LayerMask.NameToLayer("Bile");
        layerLaser = LayerMask.NameToLayer("Laser");
        layerPlayer = LayerMask.NameToLayer("Player");
        speed = initialSpeed;
		score = 0;
    }

    public bool IsPlayer() { return isPlayer; }
    public bool IsPNJ() { return !isPlayer; }

    // To pass the lead
    public void Init(GameManager gameManager, int team, int joystickID, Score humanScore) {
		this.gameManager = gameManager;
        this.isPlayer = true;
		this.humanScore = humanScore;
        this.joystickID = joystickID;
        this.input_x = "Horizontal_J" + joystickID;
        this.input_y = "Vertical_J" + joystickID;

		SetTeam(team);
	}

    public void SetTeam(int team) {
        this.team = team;
        this.anim.SetInteger("Team",team);
        this.anim.SetTrigger("TeamSwitch");
    }

    void Update() {
        if (speed < initialSpeed) {
            speed += Time.deltaTime * (initialSpeed / 3);
            speed = Mathf.Min(speed, initialSpeed);
        }

        SetVelocity();
        SetPosture();

		if (isPlayer) {
			score += Time.deltaTime * GetLength() * pointsPerHumanPerSecond;
			humanScore.SetScore(score);
        }
    }

    public int GetLength() {
        return slave == null ? 1 : slave.GetLength() + 1;
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
            angle = Vector2.Angle(Vector2.up, body.velocity);
            if (angle <= 45.0)
                anim.SetTrigger("Back");
            else if (angle >= 135.0)
                anim.SetTrigger("Front");
            else {
                anim.SetTrigger("Side");

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
            if (IsPNJ()) {
                if (master == null) {
                    Human collidedHuman = collider.gameObject.GetComponent<Human>();
                    if (collidedHuman.IsPlayer()) {
                        master = collidedHuman.SetSlave(this);
                        Utils.SetLayerToChildren(this.gameObject, LayerMask.NameToLayer("Player"));
                        SetTeam(master.team);
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

	public void HumanKilled() {
		if (gameManager != null) {
			gameManager.HumanKilled();
		} else if (master != null) {
			master.HumanKilled();
		}
	}

    void Die_Human(int death) {
		HumanKilled();

		if (slave != null) {
            slave.master = master;
            if (IsPlayer()) {
				slave.score = score;
				gameManager.SetPlayer(team, slave);
                slave.Init(gameManager, team, joystickID, humanScore);
			}
        }

        if (master != null) {
            master.slave = slave;
        }

        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();

        Transform corpse;
        rend.enabled = false;
        if (death == 1) {
            corpse = (Transform)Instantiate(BloodStain);
        } else { // (death == 2) {
            corpse = (Transform)Instantiate(Grill);
        }
        corpse.SetParent(this.transform.parent);
        corpse.position = this.transform.position;

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
        GetComponent<Rigidbody2D>().velocity = pos;
    }

    void SetVelocityPNJ() {
        if (master != null) {
            float deltaX = master.transform.position.x - this.transform.position.x;
            float deltaY = master.transform.position.y - this.transform.position.y;

            Vector2 pos = new Vector2(deltaX, deltaY) * speed;
            if (Mathf.Abs(deltaX) < 0.5 && Mathf.Abs(deltaY) < 0.5) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            } else {
				GetComponent<Rigidbody2D>().velocity = pos;
			}
        }
    }
}
