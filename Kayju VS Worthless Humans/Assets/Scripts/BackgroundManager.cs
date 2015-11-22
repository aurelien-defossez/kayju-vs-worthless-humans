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
        float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        background = (Transform)Instantiate(backgroundPrefab);
        background.position = new Vector3(0, 0, 10);
        background.parent = scene;
        background2 = (Transform)Instantiate(backgroundPrefab);
        Sprite sp = background2.GetComponent<SpriteRenderer>().sprite;
        float height = sp.texture.height / sp.pixelsPerUnit;
        background2.position = new Vector3(0, height, 0);
        background2.parent = scene;
    }

    void Update() {
        scene.position = new Vector3(scene.position.x, scene.position.y - scrollingSpeed * Time.deltaTime, 0);
    }
}
