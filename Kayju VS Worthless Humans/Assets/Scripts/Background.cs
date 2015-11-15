using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    float worldScreenHeight;
    float worldScreenWidth;
    float height;
    void Start() {
        worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        Sprite sp = GetComponent<SpriteRenderer>().sprite;
        float width = sp.texture.width / sp.pixelsPerUnit;
        height = sp.texture.height / sp.pixelsPerUnit;
        gameObject.transform.localScale = new Vector3(worldScreenWidth / width, 1, 1);
    }
    void OnBecameInvisible() {
        Debug.Log("invisibeul");
        gameObject.transform.position = new Vector3(0, (height - (worldScreenHeight / 2)) + height / 2, 10);
    }
}
