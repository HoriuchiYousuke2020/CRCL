﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    //カメラの座標をずらすための変数
    public int slide = 0;
    private int m_slide = 0;

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
        new Vector2(-12,12)
    };

    [SerializeField]
    private PlayerController[] player = new PlayerController[4];

    // Use this for initialization

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
            int targetValue = 0;
            //高いプレイヤの番号を取得
            for (int i = 0; i < Setting.PlayerNum; i++)
            {
                if (player[i].transform.position.y > player[targetValue].transform.position.y)
                {
                    targetValue = i;
                }
            }
            
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, player[targetValue].transform.position.x + 4, 60), Mathf.Lerp(transform.position.y, player[targetValue].transform.position.y + 1, 60), this.transform.position.z);

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

    public int Slide
    {
        get { return slide; }
        set { slide = value; }
    }
}
