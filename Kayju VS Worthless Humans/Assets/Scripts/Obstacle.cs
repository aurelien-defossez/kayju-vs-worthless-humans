﻿using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
    public Sprite[] sprites;
    public SpriteRenderer spriteRender;
    public Human humanPrefab;
    float timer;
    float worldScreenHeight;

    void Start() {
        timer = Random.Range(1, 3);
        spriteRender.sprite = sprites[(Random.Range(0, sprites.Length))];
        worldScreenHeight = (float)(Camera.main.orthographicSize);
    }

    void Update() {
        if (generationActive()) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                Human pnj = Instantiate(humanPrefab);
                int angle = Random.Range(0, 360);
                pnj.transform.position = new Vector3(this.transform.position.x + Mathf.Cos(angle) * 1, this.transform.position.y + Mathf.Sin(angle), 0);

                pnj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                pnj.transform.SetParent(this.transform.parent);

                timer = Random.Range(1, 3);
            }
        }
    }

    bool generationActive() {
        // obstacle is in the game zone
        return Mathf.Abs(Camera.main.transform.position.y - this.transform.position.y) < worldScreenHeight * 0.8f;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
