using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip intro, loop;

    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;

    public GameObject player;

	void Start () {
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
        if (!running)
            return;
        double time = AudioSettings.dspTime;
        if (time + 1.0F > nextEventTime) {
            audioSources[1].clip = loop;
            audioSources[1].PlayScheduled(nextEventTime);
            Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);
            running = false;
        }
	}
}
