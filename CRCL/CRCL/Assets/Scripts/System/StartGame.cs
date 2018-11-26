using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;
public class StartGame : MonoBehaviour
{
    //GamepadState keyState;
    [SerializeField]
    private PlayerController[] m_players = new PlayerController[4];

    [SerializeField]
    private GameObject[] HELICOPTER = new GameObject[4];

    private int goalCount;

    private int timer;

    // Use this for initialization
    void Start ()
    {
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        goalCount = 0;

        for (int i = 0; i < 4; i++)
        {
            if (m_players[i].GetState() == PlayerController.PLAYER_STATE.GOALED)
            {
                goalCount++;
            }
        }

        if(goalCount >= 4)
        {
            if(timer == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    HELICOPTER[i].GetComponent<Animator>().SetTrigger("Flight2");
                }
            }

            timer++;

            if(timer > 120)
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }
}
