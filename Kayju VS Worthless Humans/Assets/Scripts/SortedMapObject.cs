using UnityEngine;

public class SortedMapObject : MonoBehaviour {
    public void Update() {
        transform.position = transform.position.Z(transform.position.y);
    }
}
