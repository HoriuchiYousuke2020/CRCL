using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStageObject : MonoBehaviour
{
    [SerializeField]
    private bool STATE = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (STATE)
        {
            if (collision.transform.tag == "Player")
                if (transform.position.y < collision.transform.position.y)
                    collision.gameObject.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (STATE)
        {
            if (collision.transform.tag == "Player")
                //collision.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    collision.gameObject.transform.SetParent(null);
        }
    }
}
