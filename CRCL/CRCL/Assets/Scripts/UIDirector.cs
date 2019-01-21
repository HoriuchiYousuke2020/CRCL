using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Makoto
{
    public class UIDirector : MonoBehaviour
    {
        [SerializeField]
        private Camera Camera;

        [SerializeField]
        private GameObject[] Players = new GameObject[4];

        [SerializeField]
        private Image[] UIImages = new Image[0];

        [SerializeField]
        private Text[] UIText = new Text[0];

        [SerializeField]
        [Range(0.0f, 255.0f)]
        private float Alpha = 128.0f;

        private bool state;

        // Use this for initialization
        void Start()
        {
            state = false;
        }

        /*
         * 汎用性を持たせるならプレイヤのポジションと
         * UIのポジション、幅、高さから計算して
         * 近いオブジェクトを薄くするほうがいいかと
        */
        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Camera.transform.position.y - 3 > Players[i].transform.position.y)
                {
                    state = true;

                    break;
                }
                else
                {
                    state = false;
                }
            }

            if (state)
            {
                for (int i = 0; i < UIImages.Length; i++)
                {
                    UIImages[i].color = new Color(UIImages[i].color.r, UIImages[i].color.g, UIImages[i].color.b, Alpha / 255.0f);
                }

                state = false;
            }
            else
            {
                for (int i = 0; i < UIImages.Length; i++)
                {
                    UIImages[i].color = new Color(UIImages[i].color.r, UIImages[i].color.g, UIImages[i].color.b, 1.0f);
                }
            }
        }
    }
}

