using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class Visibility : MonoBehaviour
    {
        private bool state;

        public bool State
        {
            get { return state; }
            private set { state = value; }
        }

        // Use this for initialization
        void Start()
        {
            state = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                state = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == "Player")
            {
                state = false;
            }
        }
    }
}
