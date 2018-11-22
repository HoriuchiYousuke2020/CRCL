using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surinukeOya : MonoBehaviour {
  private  BoxCollider box;
    // Use this for initialization
    void Start () {
        box = this.GetComponent<BoxCollider>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnChildTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "OnChildTriggerEnter")
        {
            box.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "OnChildTriggerExit")
        {
            box.isTrigger = false;
        }
    }
}
