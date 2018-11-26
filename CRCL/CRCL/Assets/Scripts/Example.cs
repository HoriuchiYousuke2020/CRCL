using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public float duration  = 0.5f;
    public float magnitude = 0.5f;

    public CameraShake shake;

	// Update is called once per frame
	private void Update ()
    {
	    if(Input.GetKeyDown( KeyCode.Space))
        {
            shake.Shake(duration, magnitude);
        }	
	}
}
