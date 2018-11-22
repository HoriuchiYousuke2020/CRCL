﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;
public class ChangeScene : MonoBehaviour {
    public string m_sceneName;
    public Round round;
    private GamePad.Index m_padNum;
    GamepadState keyState;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        keyState = GamePad.GetState(m_padNum, false);
        if (Input.GetKeyDown(KeyCode.Space)|| GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any))
        {
            if(round.GetNowround() <2)
            {
                SceneManager.LoadScene(m_sceneName);
            }
            else
            {
                SceneManager.LoadScene("TitleScene");
            }
          
        }
	}
}
