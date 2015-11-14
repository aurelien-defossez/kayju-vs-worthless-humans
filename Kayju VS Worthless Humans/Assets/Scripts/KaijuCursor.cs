using UnityEngine;
using System.Collections;

public class KaijuCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Kaiju_Stomp"))
        {
            Debug.Log("Kaiju_Stomp");
        }
        if (Input.GetButtonDown("Kaiju_Laser"))
        {
            Debug.Log("Kaiju_Laser");
        }
        if (Input.GetButtonDown("Kaiju_Bile"))
        {
            Debug.Log("Kaiju_Bile");
        }
        if (Input.GetButtonDown("Kaiju_Fire"))
        {
            Debug.Log("Kaiju_Fire");
        }
    }
}
