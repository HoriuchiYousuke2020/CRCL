using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class MovePattern3 : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 10)]
        // 振り返る時間
        private float LOOK_BACK_TIME = 1.0f;

        [SerializeField]
        [Range(0, 10)]
        private float FIRING_INTERVAL = 1.0f;

        [SerializeField]
        private bool LOOK_BACK_STATE = false;

        private Firing firing;

        // 向き
        private Direction direction;

        private float lastTime;

        private float firingLastTime;

        private float currentTime;

        // Use this for initialization
        void Start()
        {
            firing = GetComponent<Firing>();

            direction = GetComponent<Direction>();

            lastTime = Time.realtimeSinceStartup;

            firingLastTime = Time.realtimeSinceStartup;
        }

        // Update is called once per frame
        void Update()
        {
            currentTime = Time.realtimeSinceStartup;

            if (LOOK_BACK_STATE)
            {
                if (currentTime - lastTime >= LOOK_BACK_TIME)
                {
                    if (direction.DIRECTION == Direction.DirectionData.LEFT)
                    {
                        direction.DIRECTION = Direction.DirectionData.RIGHT;
                    }
                    else
                    {
                        direction.DIRECTION = Direction.DirectionData.LEFT;
                    }

                    lastTime = Time.realtimeSinceStartup;
                }
            }

            if (currentTime - firingLastTime >= FIRING_INTERVAL)
            {
                firing.FiringBullet();

                firingLastTime = Time.realtimeSinceStartup;
            }
        }
    }
}
