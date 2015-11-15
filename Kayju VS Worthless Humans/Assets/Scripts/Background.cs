using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    float worldScreenHeight;
    float worldScreenWidth;
    void Start() {
        worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        Sprite sp = GetComponent<SpriteRenderer>().sprite;
        float width = sp.texture.width / sp.pixelsPerUnit;
        float height = sp.texture.height / sp.pixelsPerUnit;
        gameObject.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight*1.1f / height, 1);

    }
    void OnBecameInvisible() {
        gameObject.transform.position = new Vector3(0, 10, 10);
    }
}
