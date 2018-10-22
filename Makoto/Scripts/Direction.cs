using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class Direction : MonoBehaviour
    {
        // 向きの列挙
        public enum DirectionData
        {
            LEFT,
            RIGHT
        }

        [SerializeField]
        // 現在向いている方向
        private DirectionData m_direction;

        public DirectionData DIRECTION
        {
            get { return m_direction; }
            set { m_direction = value; }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // 向きを変更
            switch (m_direction)
            {
                case DirectionData.LEFT:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case DirectionData.RIGHT:
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
            }
        }
    }
}
