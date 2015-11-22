using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public Human humanPrefab;
    public Obstacle obstaclePrefab;
    public Transform scene;
    public int maxNbPlayers;
    public int firstPlayerIndex;
	public Range playerSpawnRange;
	public float playerSpawnY;
	public Range obstacleSpawnRange;
	public float obstacleSpawnY;
	public AudioClip[] runSounds;
    public int[] runSoundLimits;
	public Range ambientSoundInterval;

    Human[] players;
    AudioSource currentRunSound;
    float ambientSoundTimer;

    // Use this for initialization
    void Start() {
		ambientSoundTimer = ambientSoundInterval.Rand();
        players = new Human[maxNbPlayers];
    }

    // Update is called once per frame
    void Update() {
        // Connect players
        for (int i = 0; i < maxNbPlayers; i++) {
            int index = (firstPlayerIndex + i);
            if (players[i] == null && Input.GetButtonDown("Action_J" + index)) {
                Debug.Log("Player " + index + " connected.");
                Human player = Instantiate(humanPrefab);
                player.transform.position = Utils.GetScreenPosition(playerSpawnRange.Rand(), playerSpawnY);
                player.transform.SetParent(scene);
                player.SetPlayer(index);
                players[i] = player;
            }
        }

        ambientSoundTimer -= Time.deltaTime;
        if (ambientSoundTimer <= 0) {
            Obstacle obstacle = Instantiate(obstaclePrefab);
			obstacle.transform.position = Utils.GetScreenPosition(obstacleSpawnRange.Rand(), obstacleSpawnY);
            obstacle.transform.SetParent(scene);

            ambientSoundTimer = ambientSoundInterval.Rand();
		}

        // Play sound
        if (currentRunSound == null) {
            int length = 0;
            for (int i = 0; i < maxNbPlayers; i++) {
                Human player = players[i];

                if (player != null) {
                    length += player.GetLength();
                }
            }

            AudioClip source;
            if (length < runSoundLimits[0]) {
                source = runSounds[0];
            }
            else if (length < runSoundLimits[1]) {
                source = runSounds[1];
            }
            else {
                source = runSounds[2];
            }

            currentRunSound = GetComponent<AudioSource>();
            currentRunSound.clip = source;
            currentRunSound.pitch = Random.Range(.8f, 1.2f);
            currentRunSound.Play();
        }
    }
}
