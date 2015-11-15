﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoringBoard : MonoBehaviour {

    public int nb_players;
    int[] scoreI;
    public Text[] stext;
    public Text[] ftext;
    public GameObject[] switchs;

    // Use this for initialization
    void Start() {
        scoreI = new int[4];
        scoreI[0] = 0;
        scoreI[1] = 0;
        scoreI[2] = 0;
        scoreI[3] = 0;
        foreach (Text elem in stext)
            elem.text = "0";
        foreach (Text elem in ftext)
            elem.text = "0";
    }

    public void Score_up(int target) {
        scoreI[target] += 1;
        stext[target].text = scoreI[target].ToString();
        ftext[target].text = scoreI[target].ToString();
    }

    public void removePlayer(int playerNumber) {
        Debug.Log("Player " + playerNumber + " is dead, adieu Youri");
        nb_players--;
        if (nb_players <= 0)
            EndGame();
    }

    void EndGame()
    {
        foreach (GameObject elem in switchs)
            elem.SetActive(!elem.activeSelf);
    }

    // Update is called once per frame
    void Update() {

    }
}
