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

        // 状態
        private bool state;

        public bool State
        {
            get { return state; }
            private set { state = value; }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(new Vector3(-SPEED, 0, 0));
        }
    }
}
