using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;
using UnityEngine.UI;

namespace Makoto
{
    public class StartGame : MonoBehaviour
    {
        //GamepadState keyState;
        [SerializeField]
        private PlayerController m_player;

        [SerializeField]
        private GameObject[] HELICOPTER = new GameObject[3];

        [SerializeField]
        private GameObject CAMERA;

        [SerializeField]
        private TitleScenePlayer TSP;

        [SerializeField]
        private string StartSceneName;

        [SerializeField]
        private string ManualSceneName;

        [SerializeField]
        private Fade Fade;

        private int goalCount;

        private int timer;

        // Use this for initialization
        void Start()
        {
            Fade.FadeOut();
            goalCount = 0;
            timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            //keyState = GamePad.GetState(0, false);
            //if (Input.GetKeyDown(KeyCode.Space) || GamePad.GetButtonDown(GamePad.Button.Start, 0))
            //{
            //    SceneManager.LoadScene("StartScene");
            //}

            goalCount = 0;

            if (m_player.GetState() == PlayerController.PLAYER_STATE.GOALED)
            {
                goalCount++;
            }

            if (goalCount == 1)
            {
                if (timer == 0)
                {
                    switch(TSP.STATE)
                    {
                        case TitleScenePlayer.SCENE.NONE:
                            break;
                        case TitleScenePlayer.SCENE.START:
                            HELICOPTER[0].GetComponent<Animator>().SetTrigger("Flight2");
                            break;
                        case TitleScenePlayer.SCENE.EXIT:
                            HELICOPTER[1].GetComponent<Animator>().SetTrigger("Flight2");
                            break;
                        case TitleScenePlayer.SCENE.MANUAL:
                            HELICOPTER[2].GetComponent<Animator>().SetTrigger("Flight2");
                            break;
                    }

                    CAMERA.GetComponent<CameraShake>().Shake(1.5f, 0.2f);
                }

                timer++;
                
                if(timer == 130)
                {
                    Fade.FadeIn();
                }

                if (timer > 130 + (Fade.TIME * 60) + 10)
                {
                    switch (TSP.STATE)
                    {
                        case TitleScenePlayer.SCENE.NONE:
                            break;
                        case TitleScenePlayer.SCENE.START:
                            SceneManager.LoadScene(StartSceneName);
                            break;
                        case TitleScenePlayer.SCENE.EXIT:
                            Application.Quit();
                            break;
                        case TitleScenePlayer.SCENE.MANUAL:
                            SceneManager.LoadScene(ManualSceneName);
                            break;
                    }
                }
            }
        }
    }
}
