using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScore : MonoBehaviour {
    public Score[] m_players;
    public Rank[] m_playerRanks; //ゲームに参加しているプレイヤー達    
    public Score m_scoreBank;    //プレイヤー達が失ったスコアをためておく変数
    int total;                   // Use this for initialization
    void Start ()
    {
        total = 0;
        for (int i = 0; i < m_players.Length; i++)
        {
            int num;
           
            switch (m_playerRanks[i].GetRank())
            {
              
                default: break;
                case 1: num = (int)(m_scoreBank.GetScore() * 0.4); total += num; m_players[i].AddScore(num);  break;
                case 2: num = (int)(m_scoreBank.GetScore() * 0.3); total += num; m_players[i].AddScore(num);  break;
                case 3: num = (int)(m_scoreBank.GetScore() * 0.2); total += num; m_players[i].AddScore(num);  break;
                case 4: num = (int)(m_scoreBank.GetScore() * 0.1); total += num; m_players[i].AddScore(num);  break;

            }
            if (i == m_players.Length - 1)
            {
                m_scoreBank.SubScore(total);
            }
        }
        }

    // Update is called once per frame
    void Update ()
    {
		
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
