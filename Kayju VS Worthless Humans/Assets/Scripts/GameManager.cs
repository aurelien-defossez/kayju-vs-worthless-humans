using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    ArrayList players;
    Dictionary<int, int> joystickPlayerMapping;
    AudioSource currentRunSound;
    float ambientSoundTimer;

    // Use this for initialization
    void Start() {
        ambientSoundTimer = ambientSoundInterval.Rand();
        joystickPlayerMapping = new Dictionary<int, int>();
        players = new ArrayList();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Boundaries"), LayerMask.NameToLayer("Cursor"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    void Update() {
        // Connect players
        for (int i = 2; i < 8; i++) {
            if (Input.GetButtonDown("Action_J" + i)) {
                if (!joystickPlayerMapping.ContainsKey(i)) {
                    int playerNumber = players.Count;
                    Human player = Instantiate(humanPrefab);
                    player.transform.position = Utils.GetScreenPosition(playerSpawnRange.Rand(), playerSpawnY);
                    player.transform.SetParent(scene);
                    player.SetPlayer(playerNumber + 1, i);
                    joystickPlayerMapping[i] = playerNumber;
                    players.Add(player);

                    Debug.Log("Player " + playerNumber + " = Joystick " + i);
                }
                else if (players[joystickPlayerMapping[i]].Equals(null)) {
                    Human player = Instantiate(humanPrefab);
                    player.transform.position = Utils.GetScreenPosition(playerSpawnRange.Rand(), playerSpawnY);
                    player.transform.SetParent(scene);
                    player.SetPlayer(joystickPlayerMapping[i] + 1, i);
                    players[joystickPlayerMapping[i]] = player;
                    Debug.Log("Player " + joystickPlayerMapping[i] + " = Joystick " + i);
                }
            }
        }

        ambientSoundTimer -= Time.deltaTime;
        if (ambientSoundTimer <= 0) {
            Obstacle obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.position = Utils.GetScreenPosition(obstacleSpawnRange.Rand(), obstacleSpawnY);
            obstacle.transform.SetParent(scene);

            ambientSoundTimer += ambientSoundInterval.Rand();
        }

        // Play sound
        if (currentRunSound == null) {
            int length = 0;
            foreach (Human player in players) {
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
