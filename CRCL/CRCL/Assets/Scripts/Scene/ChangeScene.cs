using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;
public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private Fade Fade;

    public string m_sceneName;
    public Round round;
    private GamePad.Index m_padNum;
    GamepadState keyState;
    private bool nextSceneFlag;
    private int timer;

    // Use this for initialization
    void Start ()
    {
        Fade.FadeOut();
        round.LoadScore();
        nextSceneFlag = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        keyState = GamePad.GetState(m_padNum, false);
        if (Input.GetKeyDown(KeyCode.Space)|| GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any))
        {
            nextSceneFlag = true;
            Fade.FadeIn();
        }

        if(nextSceneFlag)
        {
            timer++;
        }

        if(timer > 40)
        {
            if (round.GetNowround() < 2)
            {
                round.SaveScore(round.GetNowround());
                SceneManager.LoadScene(m_sceneName);
            }
            else
            {
                round.SaveScore(round.GetNowround());
                SceneManager.LoadScene("TitleScene");
            }
        }
	}
}
