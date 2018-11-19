using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class BulletMove : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f, 1.0f)]
        // 速さ
        private float SPEED = 0.1f;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float RANGE = 10.0f; 

        // 状態
        private bool state;

        private Vector3 startPos;

        public bool State
        {
            get { return state; }
            private set { state = value; }
        }

        // Use this for initialization
        void Start()
        {
            startPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(new Vector3(-SPEED, 0, 0));

            if((startPos - transform.position).magnitude >= RANGE)
            {
                Destroy(transform.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
