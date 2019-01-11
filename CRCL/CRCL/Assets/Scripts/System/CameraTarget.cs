using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    //カメラの座標をずらすための変数
    public int slide = 0;
    private int m_slide = 0;
    float startTime;
    private STATE state;
    [SerializeField]
    private ScoreBank sb;
    private bool targetFlag = false;
    enum STATE
    {
        NORMAL,
        TARGET,
    }

    Vector2[] CameraLimit =
    {
        //上下
        new Vector2(110,5),
        //左右
        new Vector2(-15,20)
    };

    [SerializeField]
    private PlayerController[] player = new PlayerController[4];

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFlag == true)
        {
            for (int i = 0; i < player.Length; i++)
            {

                if (player[i].GetState() != PlayerController.PLAYER_STATE.GOALED)
                {
                    break;
                }

                if (i == player.Length - 1)
                {
                    state = STATE.TARGET;
                }
            }

            switch (state)
            {
                case STATE.NORMAL: NormalCamera(); break;
                case STATE.TARGET: Target(); break;
            }
            return;
        }

        foreach(PlayerController p in player)
        {
            if(p.transform.position.y > 5)
            {
                targetFlag = true;
            }
        }
    }

    void StartCamera()
    {

    }

    /// <summary>
    /// 通常の動作
    /// 一番上にいるプレイヤーに追従する
    /// </summary>
    void NormalCamera()
    {
        if(!sb.STATE)
        {
            int topId = SearchTop();

            CameraMove(topId);

            Limit();
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


    void Limit()
    {
        //上下左右
        if (this.transform.position.y > CameraLimit[0].x) this.transform.position = new Vector3(this.transform.position.x, CameraLimit[0].x, this.transform.position.z);
        if (this.transform.position.y < CameraLimit[0].y) this.transform.position = new Vector3(this.transform.position.x, CameraLimit[0].y, this.transform.position.z);
        if (this.transform.position.x < CameraLimit[1].x) this.transform.position = new Vector3(CameraLimit[1].x, this.transform.position.y, this.transform.position.z);
        if (this.transform.position.x > CameraLimit[1].y) this.transform.position = new Vector3(CameraLimit[1].y, this.transform.position.y, this.transform.position.z);

    }

    //高いプレイヤの番号を取得
    int SearchTop()
    {
        int topValue = 0;
        for (int i = 0; i < Setting.PlayerNum; i++)
        {
            if (player[i].transform.position.y > player[topValue].transform.position.y)
            {
                topValue = i;
            }
        }
        return topValue;
    }

    void CameraMove(int topId)
    {
        var targetpos = player[topId].transform.position;
        targetpos.x -= 4;
        var vel = Vector3.Lerp(transform.position, targetpos, 1 / 30.0f);
        vel -= transform.position;
        vel.z = 0;
        this.transform.position += vel;
    }
    //get
    //set

    public int Slide
    {
        get { return slide; }
        set { slide = value; }
    }
}
