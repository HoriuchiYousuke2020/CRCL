using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surinuke : MonoBehaviour {
 //   private BoxCollider box;
    private GameObject parent;
	// Use this for initialization
	void Start () {
      //  parent = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
            c.transform.GetComponent<BoxCollider>().isTrigger = true;
     //  parent.SendMessage("OnChildTriggerEnter", c);
        
    }
    private void OnTriggerExit(Collider c)
    {
        if(c.tag == "Player")
        c.transform.GetComponent<BoxCollider>().isTrigger = false;
       // parent.SendMessage("OnChildTriggerExit", c);
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //  if(collision.transform.position.y < this.transform.position.y)
    //    {
    //        collision.transform.GetComponent<BoxCollider>().isTrigger = true;
    //    }  
    //}

}
