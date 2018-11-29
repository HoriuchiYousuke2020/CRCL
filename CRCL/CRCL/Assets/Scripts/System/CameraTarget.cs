using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private int targetValue = 0;
    private STATE state;
    [SerializeField]
    private ScoreBank sb;

    enum STATE
    {
        NORMAL,
        TARGET,
    }

    public PlayerController[] player ;
    // Use this for initialization
    void Start()
    {
        state = STATE.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <player.Length; i++)
        {
           
            if(player[i].GetState() != PlayerController.PLAYER_STATE.GOALED)
            {
                break;
            }

            if (i == player.Length - 1)
            {
                state = STATE.TARGET;
            }
        }
        switch(state)
        {
            case STATE.NORMAL: NormalCamera(); break;
            case STATE.TARGET: Target(); break; 
        }

    }

    /// <summary>
    /// 通常の動作
    /// 一番上にいるプレイヤーに追従する
    /// </summary>
    void NormalCamera()
    {
        if(!sb.STATE)
        {
            //ｙ座標が高いオブジェクトの座標を取得
            for (int i = 0; i < player.Length; i++)
            {

                if (player[i].transform.position.y > player[targetValue].transform.position.y)
                {
                    targetValue = i;
                }

            }

            float distanceX = player[targetValue].transform.position.x - this.transform.position.x;
            float distanceY = player[targetValue].transform.position.y - this.transform.position.y;
            this.transform.position = new Vector3(this.transform.position.x + (distanceX / 10), this.transform.position.y + (distanceY / 10), this.transform.position.z);
        }
        else
        {
            foreach (Transform child in transform)
            {
                if(child.name == "CameraCollision(Clone)")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    void Target()
    {
        transform.position = new Vector3(-0.7f, 102.51f, -15f);
    }
}
