using UnityEngine;

public class AutoPosition : MonoBehaviour {
    public float x;
    public float y;

    // Use this for initialization
    void Start() {
        float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        
        transform.position = new Vector2(worldScreenWidth * x / 2f, worldScreenHeight * y / 2f);
    }
}
