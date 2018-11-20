using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisPlayItem : MonoBehaviour {
    public PlayerController m_player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<TextMesh>().text = m_player.GetItem().GetName();
    }
}
