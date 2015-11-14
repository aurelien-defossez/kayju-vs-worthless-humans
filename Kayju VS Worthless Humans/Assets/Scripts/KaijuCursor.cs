using UnityEngine;
using System.Collections;

public class KaijuCursor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetAxis("Horizontal_Kaiju") > 0)
        {
            Debug.Log("Right");
        }
        if (Input.GetAxis("Horizontal_Kaiju") < 0)
        {
            Debug.Log("Left");
        }
        if (Input.GetAxis("Vertical_Kaiju") > 0)
        {
            Debug.Log("Up");
        }
        if (Input.GetAxis("Vertical_Kaiju") < 0)
        {
            Debug.Log("Down");
        }
    }
}
