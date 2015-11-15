using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Human humanPrefab;
    public Transform scene;

    Human player2;
    Human player3;
    Human player4;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Action_J2") && player2 == null) {
            Debug.Log("Action_J2");
            player2 = Instantiate(humanPrefab);
            player2.transform.position = new Vector3(-1, 0, 0);
            player2.transform.SetParent(scene);
            player2.SetPlayer(2);
        }
        if (Input.GetButtonDown("Action_J3") && player3 == null) {
            Debug.Log("Action_J3");
            player3 = Instantiate(humanPrefab);
            player3.transform.position = new Vector3(0, 0, 0);
            player3.transform.SetParent(scene);
            player3.SetPlayer(3);
        }
        if (Input.GetButtonDown("Action_J4") && player4 == null) {
            Debug.Log("Action_J4");
            player4 = Instantiate(humanPrefab);
            player4.transform.position = new Vector3(1, 0, 0);
            player4.transform.SetParent(scene);
            player4.SetPlayer(4);
        }
    }
}
