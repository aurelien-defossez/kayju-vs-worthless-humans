using UnityEngine;
using System.Collections;

public class ScreenBoundaries : MonoBehaviour {

    public BoxCollider2D colliderNorth;
    public BoxCollider2D colliderEast;
    public BoxCollider2D colliderSouth;
    public BoxCollider2D colliderWest;

    // Use this for initialization
    void Start() {
        float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //Debug.Log(worldScreenHeight);
        //Debug.Log(worldScreenWidth);

        colliderNorth.offset= new Vector2(0,(worldScreenHeight * 0.9f / 2f));
        colliderSouth.offset = new Vector2(0, (-worldScreenHeight * 0.9f / 2f));
        colliderEast.offset = new Vector2((worldScreenWidth * 0.9f / 2f),0);
        colliderWest.offset = new Vector2((-worldScreenWidth * 0.9f / 2f), 0);

        //Debug.Log(colliderNorth.offset);
        //Debug.Log(colliderSouth.offset);
        //Debug.Log(colliderEast.offset);
        //Debug.Log(colliderWest.offset);
    }

    // Update is called once per frame
    void Update() {

    }
}
