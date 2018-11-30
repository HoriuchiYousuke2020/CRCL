using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Director : MonoBehaviour
{
    [SerializeField]
    float STAGE_TOP;

    [SerializeField]
    float STAGE_BOTTOM;

    [SerializeField]
    float STAGE_LEFT;

    [SerializeField]
    float STAGE_RIGHT;

    [SerializeField]
    GameObject[] Player = new GameObject[Setting.PLAYER_MAX];

    [SerializeField]
    GameObject Camera;

    [SerializeField]
    private bool State;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < Setting.PLAYER_MAX; i++)
        {
            if (i < Setting.PlayerNum)
            {
                Player[i].transform.position = Setting.START_PLAYER_POS[Setting.PlayerNum - Setting.PLAYER_MIN, i];
            }
            else
            {
                Player[i].SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (State)
        {
            for (int i = 0; i < Setting.PlayerNum; i++)
            {
                Vector3 pos = Player[i].transform.position;

                if (pos.y > STAGE_TOP || pos.y < STAGE_BOTTOM || pos.x < STAGE_LEFT || pos.x > STAGE_RIGHT)
                {
                    Player[i].transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, 0);
                }
            }
        }
	}
}
