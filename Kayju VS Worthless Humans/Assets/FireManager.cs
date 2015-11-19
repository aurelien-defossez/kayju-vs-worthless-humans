using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FireManager : MonoBehaviour {

    public AudioSource fireBurnSource;
    [Range(0.0f , 1.0f)]
    public float volumeDeBase;
    [Range(0.0f, 1.0f)]
    public float volumeUnitaire;
    [HideInInspector]
    public List<Transform> points;

    void Start() {
         points = new List<Transform>();
    }

	void Update () {
        if (points == null) points = new List<Transform>();

        if (points.Count > 0) {
            Vector3 barycenter;
            barycenter.x = points.Sum(p => p != null ? p.position.x : 0) / points.Count;
            barycenter.y = points.Sum(p => p != null ? p.position.y : 0) / points.Count;
            barycenter.z = points.Sum(p => p != null ? p.position.z : 0) / points.Count;

            fireBurnSource.transform.position = barycenter;
            fireBurnSource.volume = volumeDeBase + points.Count * volumeUnitaire;

            if (!fireBurnSource.isPlaying) fireBurnSource.Play();
        } else {
            fireBurnSource.Stop();
            Utils.FadeAudio(fireBurnSource, 0, 0.25f);
        }
	}

   
}
