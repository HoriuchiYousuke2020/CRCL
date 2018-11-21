using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    GameObject scoreBank;
    [SerializeField]
    private int addScore = 100;
	// Use this for initialization
	void Start () {
        scoreBank = GameObject.Find("ScoreBank");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "Player")
        {
            scoreBank.GetComponent<ScoreBank>().AddScore(addScore);
            c.transform.GetComponent<PlayerController>().GetScore().AddScore(addScore);
            Destroy(this.gameObject);
        }
    }
}
