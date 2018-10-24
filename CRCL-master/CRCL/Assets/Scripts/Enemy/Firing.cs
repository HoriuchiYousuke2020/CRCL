using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class Firing : MonoBehaviour
    {
        [SerializeField]
        private GameObject BULLET;

        private Direction direction;

        // Use this for initialization
        void Start()
        {
            direction = GetComponent<Direction>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FiringBullet()
        {
            Vector3 position = transform.position;

            if (direction.DIRECTION == Direction.DirectionData.LEFT)
            {
                position.x -= 1.0f;
            }
            else
            {
                position.x += 1.0f;
            }

            GameObject instance = Instantiate(BULLET);
            instance.transform.position = position;
            instance.GetComponent<Direction>().DIRECTION = direction.DIRECTION;
        }
    }
}
