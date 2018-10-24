using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScore : MonoBehaviour {
    public Score m_score;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
         this.GetComponent<TextMesh>().text = m_score.GetScore().ToString();
    }
}
