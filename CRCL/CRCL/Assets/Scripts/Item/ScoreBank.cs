/////2018.10.20
/////writer name is Sato Momoya
/////スコアバンク
/////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreBank : MonoBehaviour {
    public int m_subScore;               //減点ポイント
    public PlayerController[] m_players; //ゲームに参加しているプレイヤー達    
    public Score m_scoreBank;            //プレイヤー達が失ったスコアをためておく変数
    private int goalCount;    //ゴールしたカウント
    private int count;
	// Use this for initialization
	void Start ()
    {
        goalCount = 0;
        count = 0;
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
            
            if(m_players[i].GetState() == (int)PlayerController.PLAYER_STATE.GOAL)
            {
                //int num;
                //switch(goalCount)
                //{

                //    default:break;
                //    case 0:num = (int)(m_scoreBank.GetScore() * 0.4); m_players[i].GetScore().AddScore(num); m_scoreBank.SubScore(num) ; break;
                //    case 1: num = (int)(m_scoreBank.GetScore() * 0.3); m_players[i].GetScore().AddScore(num); m_scoreBank.SubScore(num); break;
                //    case 2: num = (int)(m_scoreBank.GetScore() * 0.2); m_players[i].GetScore().AddScore(num); m_scoreBank.SubScore(num); break;
                //    case 3: num = (int)(m_scoreBank.GetScore() * 0.1); m_players[i].GetScore().AddScore(num); m_scoreBank.SubScore(num); break;

                //}
                goalCount++;
               
            }
           
        }

        if (goalCount > 0)
        {
            count++;
        }

        if (count > 600)
        {
            count = 0;
            SceneManager.LoadScene("ResultScene");

        }
    }

    public Score GetScore()
    {
        return m_scoreBank;
    }

    public void AddScore(int addnum)
    {
        m_scoreBank.AddScore(addnum);
    }
}
