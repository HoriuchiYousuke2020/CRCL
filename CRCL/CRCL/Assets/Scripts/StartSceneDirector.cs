using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Makoto
{
    public class StartSceneDirector : MonoBehaviour
    {
        [SerializeField]
        private Text CountDownText;

        [SerializeField]
        [Range(1,10)]
        private int CountDownTime = 3;

        [SerializeField]
        private Fade Fade;

        private float startTime;
        private float currentTime;

        // Use this for initialization
        void Start()
        {
            Fade.FadeOut();
            startTime = Time.realtimeSinceStartup;
        }

        // Update is called once per frame
        void Update()
        {
            if (Fade.STATE == Fade.State.SEE_THROUGH)
            {
                currentTime = Time.realtimeSinceStartup;

                float time = (CountDownTime + 1) - ((currentTime - startTime));

                if (time < 1.0f && time > 0.5f)
                {
                    CountDownText.text = "GO";
                }
                else if (time < 0.5f)
                {
                    SceneManager.LoadScene("GameScene");
                }
                else
                {
                    CountDownText.text = ((int)time).ToString();
                }
            }
            else
            {
                startTime = Time.realtimeSinceStartup;
            }
        }
    }
}
