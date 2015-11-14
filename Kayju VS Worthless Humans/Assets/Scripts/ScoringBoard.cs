using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoringBoard : MonoBehaviour {

    public Dictionary<string, int> score;

	// Use this for initialization
	void Start () {
        score = new Dictionary<string, int>();
        score["Kaiju"] = 0;
        score["J2"] = 0;
        score["J3"] = 0;
        score["J4"] = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Kaiju score : [" + score["Kaiju"] +"]");
	}
}
