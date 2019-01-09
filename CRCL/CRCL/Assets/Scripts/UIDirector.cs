using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Makoto
{
    public class UIDirector : MonoBehaviour
    {
        [SerializeField]
        private Camera Camera;

        [SerializeField]
        private GameObject[] Player = new GameObject[4];

        [SerializeField]
        private TextMesh[] Score = new TextMesh[2];

        [SerializeField]
        private TextMesh[] Item = new TextMesh[2];

        [SerializeField]
        [Range(0.0f, 255.0f)]
        private float Alpha = 128.0f;

        private bool state;

        // Use this for initialization
        void Start()
        {
            //state = false;
        }

        // Update is called once per frame
        void Update()
        {
            //for (int i = 0; i < 4; i++)
            //{
            //    if (Camera.transform.position.x + 5 < Player[i].transform.position.x)
            //    {
            //        state = true;

            //        break;
            //    }
            //    else
            //    {
            //        state = false;
            //    }
            //}

            //if (state)
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        Score[i].color = new Color(Score[i].color.r, Score[i].color.g, Score[i].color.b, Alpha / 255.0f);
            //        Item[i].color = new Color(Item[i].color.r, Item[i].color.g, Item[i].color.b, Alpha / 255.0f);
            //    }

            //    state = false;
            //}
            //else
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        Score[i].color = new Color(Score[i].color.r, Score[i].color.g, Score[i].color.b, 1.0f);
            //        Item[i].color = new Color(Item[i].color.r, Item[i].color.g, Item[i].color.b, 1.0f);
            //    }
            //}          
        }
    }
}

