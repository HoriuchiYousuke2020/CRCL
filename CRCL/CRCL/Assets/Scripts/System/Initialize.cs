using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

    public Score[] score;
    public Round round;
	// Use this for initialization
	void Start ()
    {
		for(int i= 0;i < score.Length; i++)
        {
            score[i].SetScore(0);
        }
        round.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
