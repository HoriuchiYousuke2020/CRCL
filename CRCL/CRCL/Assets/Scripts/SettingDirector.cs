using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Makoto
{
    public class SettingDirector : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Players = new GameObject[4];

        [SerializeField]
        private Material DefaultMaterial;

        [SerializeField]
        private Material[] PlayerMaterial = new Material[4];

        [SerializeField]
        private Text PlayerNumText;

        [SerializeField]
        private string NextSceneName;

        [SerializeField]
        private Fade Fade;

        private int lastControllerNum;

        private int currentControllerNum;

        public int PLAYER_NUM
        {
            get { return currentControllerNum; }
        }

        private bool sceneLoadFlag;

        private int timer;

        [SerializeField]
        [Range(0, 4)]
        private int testNum = 0;

        // Use this for initialization
        void Start()
        {
            //lastControllerNum = Input.GetJoystickNames().Length;
            lastControllerNum = testNum;

            Fade.FadeOut();

            sceneLoadFlag = false;

            timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(!sceneLoadFlag)
            {
                //currentControllerNum = Input.GetJoystickNames().Length;
                currentControllerNum = testNum;
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    testNum--;
                    if (testNum < 0)
                    {
                        testNum = 0;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    testNum++;
                    if(testNum > 4)
                    {
                        testNum = 4;
                    }
                }

                PlayerNumText.text = "現在の人数は" + currentControllerNum + "人です";

                for (int i = 0; i < 4; i++)
                {
                    if (i < currentControllerNum)
                    {
                        Players[i].GetComponent<Renderer>().material = PlayerMaterial[i];
                    }
                    else
                    {
                        Players[i].GetComponent<Renderer>().material = DefaultMaterial;
                    }
                }

                lastControllerNum = currentControllerNum;

                if (Input.GetKeyDown(KeyCode.Space) && currentControllerNum >= Setting.PLAYER_MIN)
                {
                    sceneLoadFlag = true;
                    Fade.FadeIn();
                }
            }
            else
            {
                timer++;

                if(timer > Fade.TIME * 60 + 10)
                {
                    Setting.PlayerNum = currentControllerNum;
                    SceneManager.LoadScene(NextSceneName);
                }
            }
        }
    }
}
