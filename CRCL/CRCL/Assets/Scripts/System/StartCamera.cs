using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(0, 105, -15);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > 5)
            this.transform.position = new Vector3(this.transform.position.x, transform.position.y - 0.3f, -15);
    }
}