using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    //true = hight : false = width
    private bool state;
    private float speed;
	// Use this for initialization
	void Start ()
    {
		if(this.transform.localScale.x < this.transform.localScale.y)
        {
            state = true;
        }
        else
        {
            state = false;
        }
        speed = 0.1f;
    }
    
    // Update is called once per frame
    void Update ()
    {
        Vector3 pos = this.transform.position;
        
        switch(state)
        {
            case true:  pos.y += speed;  break;
            case false: pos.x += speed;  break;
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
