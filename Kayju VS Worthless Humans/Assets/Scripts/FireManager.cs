using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FireManager : MonoBehaviour {
    public AudioSource fireBurnSource;
    [Range(0.0f , 1.0f)]
    public float volumeDeBase;
    [Range(0.0f, 1.0f)]
    public float volumeUnitaire;

	private List<Fire> fires;
	private bool fireStarted;

    void Start() {
		fires = new List<Fire>();
		fireStarted = false;
    }

	public void AddFire(Fire fire) {
		fires.Add(fire);
    }
	
	public void RemoveFire(Fire fire) {
		fires.Remove(fire);
	}

	void Update () {
        if (fires.Count > 0) {
            Vector3 barycenter = Vector3.zero;
			foreach (Fire fire in fires) {
				barycenter += fire.transform.position;
			}

			barycenter = new Vector3(barycenter.x / fires.Count, barycenter.y / fires.Count, 0);

            fireBurnSource.transform.position = barycenter;
            fireBurnSource.volume = volumeDeBase + fires.Count * volumeUnitaire;

			if (!fireBurnSource.isPlaying) {
				fireStarted = true;
                fireBurnSource.Play();
			}
        } else if (fireStarted) {
			fireStarted = false;
            fireBurnSource.Stop();
            Utils.FadeAudio(fireBurnSource, 0, 0.25f);
        }
	}
}
