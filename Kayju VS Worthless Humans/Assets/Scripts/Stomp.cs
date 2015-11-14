using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Stomp : MonoBehaviour {
    public Transform shadow;
	public Transform fist;
    public float maxSize;
    public Timeline timeline;
    public Range shadowOpacity;
    public CircleCollider2D hitCollider;
    public CircleCollider2D blockCollider;
	public Vector2 initialFistPosition;

    private SpriteRenderer shadowSprite;

    public void Start() {
        shadowSprite = shadow.GetComponent<SpriteRenderer>();
		initialFistPosition = fist.position;

        timeline.Play();
	}

	[TimelineMethod]
	public void GrowShadow(TimelineCall options) {
		shadow.localScale = new Vector2(1, 1) * options.progress * maxSize;
		shadowSprite.color = shadowSprite.color.A(shadowOpacity.Lerp(options.progress));
	}

	[TimelineMethod]
	public void MoveFist(TimelineCall options) {
		fist.position = Vector2.Lerp(initialFistPosition, transform.position,
			options.parameter == 0 ? options.progress : 1 - options.progress);
	}

	[TimelineMethod]
    public void Hit(TimelineCall options)
    {
        hitCollider.enabled=true;
		shadowSprite.enabled = false;
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
