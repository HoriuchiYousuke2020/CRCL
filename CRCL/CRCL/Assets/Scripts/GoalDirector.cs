using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Makoto
{
    public class GoalDirector : MonoBehaviour
    {
        [SerializeField]
        private Material[] MATERIAL = new Material[4];

        [SerializeField]
        private GameObject[] HELICOPTER = new GameObject[4];

        public List<PlayerController> player;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (player.Count != 0)
            {
                for (int i = 0; i < player.Count; i++)
                {
                    if (player[i].PLAYER_NUM != 0)
                    {
                        int count = 0;
                        foreach (Transform child in HELICOPTER[i].transform)
                        {
                            //child is your child transform

                            child.GetComponent<Renderer>().material = MATERIAL[player[i].PLAYER_NUM - 1];
                            count++;
                        }
                    }
                }
            }
        }
    }
}
