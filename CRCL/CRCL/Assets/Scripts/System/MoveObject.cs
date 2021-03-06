﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float startSpeed = 0.1f;
    //true = hight : false = width
    [SerializeField]
    private bool state = true;
    private float speed;
    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        speed = startSpeed;
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        Vector3 pos = this.transform.position;

        switch (state)
        {
            case true: pos.y += speed; break;
            case false: pos.x += speed; break;
        }

        this.transform.position = pos;
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "MoveRange")
        {
            speed = -speed;
        }
    }
}
