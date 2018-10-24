using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class NotFall : MonoBehaviour
    {
        [SerializeField]
        // 落ちるか落ちないか
        private bool FoolState;

        // 向き
        Direction direction;

        // Use this for initialization
        void Start()
        {
            direction = GetComponent<Direction>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerExit(Collider other)
        {
            if (!FoolState)
            {
                if (other.tag == "StageObject")
                {
                    if (direction.DIRECTION == Direction.DirectionData.LEFT)
                    {
                        direction.DIRECTION = Direction.DirectionData.RIGHT;
                    }
                    else
                    {
                        direction.DIRECTION = Direction.DirectionData.LEFT;
                    }

                    transform.Translate(new Vector3(0.1f, 0, 0));
                }
            }
        }
    }
}
