using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class Money : MonoBehaviour
    {
        const int MIN = 0;
        const int MAX = 1000;

        [SerializeField]
        [Range(MIN, MAX)]
        // 持っているお金
        private int value;

        public int Value
        {
            get { return value; }
            private set { this.value = value; }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
