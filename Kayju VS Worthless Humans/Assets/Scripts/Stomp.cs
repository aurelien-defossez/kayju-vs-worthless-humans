using UnityEngine;

public class Stomp : MonoBehaviour {
    public Transform shadow;
	public Transform fist;
	public GameObject dust;
    public Timeline timeline;
    public Range shadowOpacity;
    public CircleCollider2D hitCollider;
    public CircleCollider2D blockCollider;
	public Vector2 initialFistPosition;
    //public AudioClip stompPreparationSound;
    //public AudioClip stompHitSound;
	public float maxSceneShake;

    private SpriteRenderer shadowSprite;
	private Transform scene;

	public void Init(Transform scene) {
		this.scene = scene;

		dust.SetActive(false);
		shadowSprite = shadow.GetComponentInChildren<SpriteRenderer>();
		initialFistPosition = fist.position;
		transform.position = transform.position.Z(0);

		timeline.Play();

        //Utils.PlayPitchedClipAt(stompPreparationSound, transform.position);
        transform.FindChild("Anticipation").GetComponent<AudioSource>().Play();
	}

	[TimelineMethod]
	public void GrowShadow(TimelineCall options) {
		if (options.parameter == 1) {
			options.progress = 1 - options.progress;
		}

		shadow.localScale = new Vector2(1, 1) * options.progress;
		shadowSprite.color = shadowSprite.color.A(shadowOpacity.Lerp(options.progress));
	}

	[TimelineMethod]
	public void MoveFist(TimelineCall options) {
		Vector2 position2d = Vector2.Lerp(initialFistPosition, transform.position,
			options.parameter == 0 ? options.progress : 1 - options.progress);

        fist.position = new Vector3(position2d.x, position2d.y, 0);
	}

	[TimelineMethod]
    public void Hit(TimelineCall options){
		shadowSprite.enabled = false;
        hitCollider.enabled=true;
        //Utils.PlayPitchedClipAt(stompHitSound, transform.position);
        transform.FindChild("Hit").GetComponent<AudioSource>().Play();
		dust.SetActive(true);
	}

	[TimelineMethod]
	public void Shake(TimelineCall options) {
		scene.position = scene.position.X(Random.Range(-maxSceneShake, maxSceneShake));
	}

	[TimelineMethod]
	public void HitEnd(TimelineCall options) {
		hitCollider.enabled = false;
		blockCollider.enabled = true;
		scene.position = scene.position.X(0);
	}

	[TimelineMethod]
	public void DustEnd(TimelineCall options) {
		dust.SetActive(false);
	}

	[TimelineMethod]
    public void Shutdown(TimelineCall options)
    {
        Destroy(gameObject);
    }
}
