using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
    public Transform backgroundPrefab;
    public Transform scene;
    public float scrollingSpeed;

    // Use this for initialization
    Transform background;
    Transform background2;
    void Start() {
        background = (Transform)Instantiate(backgroundPrefab);
        background.parent = scene;
        background.position = new Vector3(0, 0, 10);
        background2 = (Transform)Instantiate(backgroundPrefab);
        background2.parent = scene;
        background2.position = new Vector3(0, 10, 10);
    }
    void Update() {
        scene.position = new Vector3(scene.position.x, scene.position.y - scrollingSpeed * Time.deltaTime, 0);
    }
}
