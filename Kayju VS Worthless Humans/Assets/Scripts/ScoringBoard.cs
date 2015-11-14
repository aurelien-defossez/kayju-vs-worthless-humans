using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoringBoard : MonoBehaviour {

    public Dictionary<string, int> score;
    public GUIText scoreText;
    int stext;

    // Use this for initialization
    void Start () {
        score = new Dictionary<string, int>();
        score["Kaiju"] = 0;
        score["J2"] = 0;
        score["J3"] = 0;
        score["J4"] = 0;
        stext = 0;
        scoreText.text = "LORD LIZARD DESTROY NONE OF THOSE WORTHLESS HUMANS !!";
    }
	
	// Update is called once per frame
	void Update () {
        if (stext != score["Kaiju"])
        {
            stext = score["Kaiju"];
            scoreText.text = "LORD LIZARD DESTROY " + stext + " OF THOSE WORTHLESS HUMANS !!";
            Debug.Log("SCORE UPDATED !!! SCORE IS NOW OF " + stext);
        }
    }
}
