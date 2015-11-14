using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
    public Transform backgroundPrefab;
    // Use this for initialization
    Transform background;
    Transform background2;
    void Start() {
        background = (Transform)Instantiate(backgroundPrefab);
        background.position = new Vector3(0, 0, 10);
        background2 = (Transform)Instantiate(backgroundPrefab);
        background2.position = new Vector3(0, 10, 10);
    }
}
