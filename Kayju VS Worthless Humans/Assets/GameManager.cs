using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Human humanPrefab;
    public Transform scene;
	public int maxNbPlayers;
	public int firstPlayerIndex;
	public Range spawnRange;

	Human[] players;

    // Use this for initialization
    void Start() {
		players = new Human[maxNbPlayers];
    }

    // Update is called once per frame
    void Update() {
		for (int i = 0; i < maxNbPlayers; i++) {
			int index = (firstPlayerIndex + i);
            if (players[i] == null && Input.GetButtonDown("Action_J" + index)) {
				Debug.Log("Player " + index + " connected.");
				Human player = Instantiate(humanPrefab);
				player.transform.position = new Vector3(spawnRange.Rand(), 0, 0);
				player.transform.SetParent(scene);
				player.SetPlayer(2);
			}
		}
    }
}
