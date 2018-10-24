using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class Testmove : MonoBehaviour
    {
        Rigidbody rb;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-0.03f, 0, 0));
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(0.03f, 0, 0));
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, 550, 0));
            }
        }
    }
}
