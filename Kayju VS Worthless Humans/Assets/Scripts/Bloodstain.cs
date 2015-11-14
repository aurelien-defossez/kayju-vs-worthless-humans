using UnityEngine;
using System.Collections;

public class Bloodstain : MonoBehaviour {

    float deadTimer = 0;
    public float time = 3;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        deadTimer += Time.deltaTime;
        if (deadTimer >= time)
            Destroy(gameObject);
    }

}
