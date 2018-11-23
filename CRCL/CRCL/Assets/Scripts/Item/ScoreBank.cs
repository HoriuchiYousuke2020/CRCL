/////2018.10.20
/////writer name is Sato Momoya
/////スコアバンク
/////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBank : MonoBehaviour
{
    public int m_subScore;               //減点ポイント
    public PlayerController[] m_players; //ゲームに参加しているプレイヤー達    
    public Score m_scoreBank;            //プレイヤー達が失ったスコアをためておく変数
    private int goalCount;
    private int count;
    public Round round;
    [SerializeField]
    private int RESULT_SCENE_COUNT = 600;

    [SerializeField]
    private GameObject[] HELICOPTER = new GameObject[4];


    // Use this for initialization
    void Start ()
    {
        goalCount = 1;
        count = 0;
        //m_scoreBank.SetScore(0);
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
            

            if(m_players[i].GetState() == PlayerController.PLAYER_STATE.GOAL)
            {
                m_players[i].GetRank().DetermineRANK(goalCount);
                //int num;
               
                goalCount++;
               
            }
           
        }

        if (goalCount > 1)
        {
            count++;
        }

        if (count == RESULT_SCENE_COUNT)
        {
            for (int i = 0; i < 4; i++)
            {
                HELICOPTER[i].GetComponent<Animator>().SetTrigger("Flight");
            }
        }
        else if (count > RESULT_SCENE_COUNT + 120)
        {
            round.AddCount();
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
