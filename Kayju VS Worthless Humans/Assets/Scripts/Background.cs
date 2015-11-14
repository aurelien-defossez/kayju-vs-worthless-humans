using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    public float scrollingSpeed;
    bool hasBeenSeen;
    void Start() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * scrollingSpeed;
        hasBeenSeen = false;
    }

    void OnBecameVisible() {
        hasBeenSeen = true;
    }

    void OnBecameInvisible() {
        if (hasBeenSeen) {
            gameObject.transform.position = new Vector3(0, 10, 10);
        }
    }
}
