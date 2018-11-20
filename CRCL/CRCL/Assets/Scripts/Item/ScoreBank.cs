﻿/////2018.10.20
/////writer name is Sato Momoya
/////スコアバンク
/////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBank : MonoBehaviour {
    public int m_subScore;               //減点ポイント
    public PlayerController[] m_players; //ゲームに参加しているプレイヤー達    
    public Score m_scoreBank;            //プレイヤー達が失ったスコアをためておく変数
	// Use this for initialization
	void Start ()
    {
        m_scoreBank.SetScore(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i< m_players.Length; i++)
        {
            if (m_players[i].GetDownFlag())
            {
                int a = m_players[i].GetScore().GetScore();
                m_players[i].GetScore().SubScore(m_subScore);
                m_scoreBank.AddScore(a - m_players[i].GetScore().GetScore());

                m_players[i].SetDownFlag(false);
            }
            

            
        }
	}
}