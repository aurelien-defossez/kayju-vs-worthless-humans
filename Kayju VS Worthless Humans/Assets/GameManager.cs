using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public Human humanPrefab;
    public Transform scene;
    public int maxNbPlayers;
    public int firstPlayerIndex;
    public Range spawnRange;
	public AudioClip[] runSounds;

	Human[] players;
	AudioSource currentRunSound;
	 float timer;
    float worldScreenHeight;
    float worldScreenWidth;

    // Use this for initialization
    void Start() {
        timer = Random.Range(1, 3);
        players = new Human[maxNbPlayers];
        worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        worldScreenHeight *= 0.9f;
        worldScreenWidth *= 0.72f;
    }

    // Update is called once per frame
    void Update() {
		// Connect players
		for (int i = 0; i < maxNbPlayers; i++) {
			int index = (firstPlayerIndex + i);
            if (players[i] == null && Input.GetButtonDown("Action_J" + index)) {
				Debug.Log("Player " + index + " connected.");
				Human player = Instantiate(humanPrefab);
				player.transform.position = new Vector3(spawnRange.Rand(), 0, 0);
				player.transform.SetParent(scene);
				player.SetPlayer(index);
			}
		}

		// Play sound
		if (currentRunSound == null) {
			currentRunSound = Utils.PlayPitchedClipAt(runSounds[0], gameObject, Random.Range(0.8f, 1.2f));
			currentRunSound.volume = 0.2f;
        }

        timer -= Time.deltaTime;

        if (timer <= 0) {

            Human pnj = Instantiate(humanPrefab);

            pnj.transform.position = new Vector3(Random.Range(-worldScreenWidth / 2, worldScreenWidth / 2), Random.Range(-worldScreenHeight / 2, worldScreenHeight / 2), 0);
            pnj.transform.SetParent(scene);


            timer = Random.Range(1, 3);
        }

    }
}
