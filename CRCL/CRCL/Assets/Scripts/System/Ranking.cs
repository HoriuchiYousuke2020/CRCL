using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    private Material[] m_material;
    private int m_rank = 1;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            m_material[m_rank].color = c.GetComponent<PlayerController>().GetColor();
            m_rank++;
        }
    }
}
