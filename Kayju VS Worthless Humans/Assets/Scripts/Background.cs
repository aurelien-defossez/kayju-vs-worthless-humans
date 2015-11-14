using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    void OnBecameInvisible() {
        gameObject.transform.position = new Vector3(0,10,10);
    }
}
