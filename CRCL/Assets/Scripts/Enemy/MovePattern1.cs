using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class MovePattern1 : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0f, 1.0f)]
        // 速さ
        private float DEFAULT_SPEED;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float SPECIAL_SPEED;

        // 視角
        private Visibility visibility;

        // Use this for initialization
        void Start()
        {
            visibility = transform.Find("Vision").GetComponent<Visibility>();
        }

        // Update is called once per frame
        void Update()
        {
            // 速度
            Vector3 velocity;

            if(visibility.State)
            {
                velocity = new Vector3(-SPECIAL_SPEED , 0, 0);
            }
            else
            {
                velocity = new Vector3(-DEFAULT_SPEED, 0, 0);
            }

            // 移動
            transform.Translate(velocity);
        }
    }
}
