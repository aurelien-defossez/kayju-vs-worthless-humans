using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
    public AudioClip intro, loop;
	public GameObject player;

	private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;

	void Awake () {
        int i = 0;
        while (i < 2) {
            var child = Instantiate(player);
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.GetComponent<AudioSource>();
            i++;
        }

        audioSources[0].clip = intro;
        audioSources[0].Play();

        audioSources[1].loop = true;

        nextEventTime = AudioSettings.dspTime + intro.length;
        running = true;
    }
	
	void Update () {
        if (running) {
			double time = AudioSettings.dspTime;

			if (time + 1.0F > nextEventTime) {
				audioSources[1].clip = loop;
				audioSources[1].PlayScheduled(nextEventTime);
				running = false;
			}
		}
	}
}
