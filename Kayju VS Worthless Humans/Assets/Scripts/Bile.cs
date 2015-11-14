using UnityEngine;
using System.Collections;

public class Bile : MonoBehaviour {

    public Timeline timeline;
    public CircleCollider2D hitCollider;

    // Use this for initialization
    void Start() {
        timeline.Play();
    }

    [TimelineMethod]
    public void Hit(TimelineCall options) {
        hitCollider.enabled = true;
    }

    [TimelineMethod]
    public void Shutdown(TimelineCall options) {
        Destroy(gameObject);
    }
}
