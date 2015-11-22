using UnityEngine;
using System.Collections;

public class ScreenBoundaries : MonoBehaviour {
    public BoxCollider2D colliderNorth;
    public BoxCollider2D colliderEast;
    public BoxCollider2D colliderSouth;
    public BoxCollider2D colliderWest;

	public float verticalOffset;
	public float horizontalOffset;

    // Use this for initialization
    void Start() {
		colliderNorth.offset = Utils.GetScreenPosition(0, verticalOffset);
		colliderSouth.offset = Utils.GetScreenPosition(0, -verticalOffset);
		colliderEast.offset = Utils.GetScreenPosition(horizontalOffset, 0);
		colliderWest.offset = Utils.GetScreenPosition(-horizontalOffset, 0);
    }
}
