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
    GameObject[] Player = new GameObject[4];

    [SerializeField]
    GameObject Camera;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < 4; i++)
        {
            Vector3 pos = Player[i].transform.position;

            if(pos.y > STAGE_TOP || pos.y < STAGE_BOTTOM || pos.x < STAGE_LEFT || pos.x > STAGE_RIGHT)
            {
                Player[i].transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, 0);
            }
        }
	}
}
