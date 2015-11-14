using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    bool hasBeenSeen;
    void Start() {
        hasBeenSeen = false;
    }

    void OnBecameVisible() {
        hasBeenSeen = true;
    }

    void OnBecameInvisible() {
        if (hasBeenSeen) {
            Debug.Log(gameObject.transform.position);
            gameObject.transform.position = new Vector3(0,10,10);
            Debug.Log(gameObject.transform.position);
        }
    }
}
