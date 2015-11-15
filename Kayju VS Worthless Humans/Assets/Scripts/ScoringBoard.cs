using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoringBoard : MonoBehaviour {

    public Dictionary<string, int> score;
    int[]  scoreI;
    public Text[] stext;

    // Use this for initialization
    void Start () {
        scoreI = new int[4];
        scoreI[0] = 0;
        scoreI[1] = 0;
        scoreI[2] = 0;
        scoreI[3] = 0;
        foreach (Text elem in stext)
            elem.text = "0";
    }
	
    public void Score_up(int target)
    {
        scoreI[target] += 1;
        stext[target].text = scoreI[target].ToString();
    }

	// Update is called once per frame
	void Update () {

    }
}
