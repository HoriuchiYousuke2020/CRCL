using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;
public class StartGame : MonoBehaviour {
    GamepadState keyState;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        keyState = GamePad.GetState(0, false);
        if (Input.GetKeyDown(KeyCode.Space) || GamePad.GetButtonDown(GamePad.Button.Start, 0))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
