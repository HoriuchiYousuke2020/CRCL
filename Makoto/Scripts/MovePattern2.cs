using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class MovePattern2 : MonoBehaviour
    {
        private enum State
        {
            SEARCH,
            STAND_BY,
            FIRING,
        }

        [SerializeField]
        private GameObject rays;

        [SerializeField]
        [Range(1, 10)]
        // 振り返る時間
        private int LOOK_BACK_TIME = 1;

        [SerializeField]
        [Range(0, 10)]
        private int FIRING_INTERVAL = 1;

        private State state;

        private Firing firing;

        // 向き
        private Direction direction;

        private Visibility visibility;

        private float lastTime;

        private float currentTime;

        // Use this for initialization
        void Start()
        {
            state = State.SEARCH;

            firing = GetComponent<Firing>();

            direction = GetComponent<Direction>();

            visibility = transform.Find("Vision").GetComponent<Visibility>();

            lastTime = Time.realtimeSinceStartup;
        }

        // Update is called once per frame
        void Update()
        {
            currentTime = Time.realtimeSinceStartup;

            switch(state)
            {
                case State.SEARCH:

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

                    if (visibility.State)
                    {
                        rays.SetActive(true);

                        state = State.STAND_BY;
                    }
                    break;

                case State.STAND_BY:

                    if (!visibility.State)
                    {
                        rays.SetActive(false);

                        state = State.SEARCH;
                    }
                    else if (currentTime - lastTime >= FIRING_INTERVAL)
                    {
                        state = State.FIRING;
                    }
                    break;

                case State.FIRING:

                    firing.FiringBullet();

                    lastTime = Time.realtimeSinceStartup;

                    state = State.STAND_BY;
                    break;
            }
        }
    }
}
