using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public static int PlayerNum = 0;
    private static Vector3[,] StartPlayerPos = new Vector3[3, 4] { { new Vector3(-2.0f, 1.0f, 0.0f), new Vector3(2.0f, 1.0f, 0.0f), new Vector3(), new Vector3() },
                                                                  { new Vector3(-8.0f, 1.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(8.0f, 1.0f, 0.0f), new Vector3() },
                                                                  { new Vector3(-6.0f, 1.0f, 0), new Vector3(-2.0f, 1.0f, 0.0f), new Vector3(2.0f, 1.0f, 0.0f), new Vector3(6.0f, 1.0f, 0.0f) } };

    public static Vector3[,] START_PLAYER_POS
    {
        get { return StartPlayerPos; }
    }


    public static int PLAYER_MAX
    {
        get { return 4; }
    }

    public static int PLAYER_MIN
    {
        get { return 2; }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
