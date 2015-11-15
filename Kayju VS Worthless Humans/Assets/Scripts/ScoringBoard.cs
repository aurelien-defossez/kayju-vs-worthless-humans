using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoringBoard : MonoBehaviour {

    public Dictionary<string, int>  score;
    public Transform                TextObject;
    Text                            scoreText;
    int                             stext;

    // Use this for initialization
    void Start () {
        score = new Dictionary<string, int>();
        score["Kaiju"] = 0;
        score["J2"] = 0;
        score["J3"] = 0;
        score["J4"] = 0;
        stext = 0;
        scoreText = TextObject.GetComponent<Text>();
        scoreText.text = "0";
    }
	
	// Update is called once per frame
	void Update () {
        if (scoreText != null &&
            stext != score["Kaiju"])
        {
            stext = score["Kaiju"];
            scoreText.text = stext.ToString();
        }
    }
}
