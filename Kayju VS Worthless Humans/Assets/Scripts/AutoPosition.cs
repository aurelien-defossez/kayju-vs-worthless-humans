using UnityEngine;

public class AutoPosition : MonoBehaviour {
    public float x;
    public float y;

    // Use this for initialization
    void Start() {
        transform.position = Utils.GetScreenPosition(x, y);
    }
}
