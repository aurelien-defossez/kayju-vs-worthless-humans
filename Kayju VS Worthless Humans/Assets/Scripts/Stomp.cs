using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Stomp : MonoBehaviour {
    public Transform shadow;
    public float maxSize;
    public Timeline timeline;
    public Range shadowOpacity;
    public CircleCollider2D hitCollider;
    public CircleCollider2D blockCollider;


    private SpriteRenderer shadowSprite;

    public void Start() {
        shadowSprite = shadow.GetComponent<SpriteRenderer>();

        timeline.Play();
    }

    [TimelineMethod]
    public void GrowShadow(TimelineCall options) {
        shadow.localScale = new Vector2(1, 1) * options.progress * maxSize;
        shadowSprite.color = shadowSprite.color.A(shadowOpacity.Lerp(options.progress));
    }
    [TimelineMethod]
    public void Hit(TimelineCall options)
    {
        hitCollider.enabled=true;
    }
    [TimelineMethod]
    public void HitEnd(TimelineCall options)
    {
        hitCollider.enabled = false;
        blockCollider.enabled = true;
    }

    [TimelineMethod]
    public void Shutdown(TimelineCall options)
    {
        Destroy(gameObject);
    }
}
