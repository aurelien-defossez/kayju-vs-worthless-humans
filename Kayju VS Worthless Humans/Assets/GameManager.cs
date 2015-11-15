using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int numberOfPlayers;
    public Human humanPrefab;
    public Transform scene;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < numberOfPlayers; i++) {
            Human h = Instantiate(humanPrefab);

            h.transform.position = new Vector3(i - 1, 0, 0);
            h.transform.SetParent(scene);
            h.SetPlayer(i + 2);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
