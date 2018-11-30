/////2018.10.20
/////writer name is Sato Momoya
/////スコアバンク
/////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ScoreBank : MonoBehaviour
{
    public int m_subScore;               //減点ポイント
    public PlayerController[] m_players = new PlayerController[4]; //ゲームに参加しているプレイヤー達    
    public Score m_scoreBank;            //プレイヤー達が失ったスコアをためておく変数
    private int goalCount;
    private int count;
    public Round round;
    [SerializeField]
    private int RESULT_SCENE_COUNT = 600;

    [SerializeField]
    private GameObject[] HELICOPTER = new GameObject[4];

    [SerializeField, Range(0.0f, 10.0f)]
    private float m_time = 0.0f;

    [SerializeField]
    private Vector3 m_targetPosition = Vector3.zero;

    [SerializeField]
    private GameObject CAMERA;

    [SerializeField]
    private Fade Fade;

    private float m_startTime = 0.0f;
    private Vector3 m_startPosition = Vector3.zero;

    private bool state = false;

    public bool STATE
    {
        get { return state; }
    }

    // Use this for initialization
    void Start ()
    {
        goalCount = 1;
        count = 0;
        round.LoadScore();
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

        if(count == RESULT_SCENE_COUNT - 120)
        {
            m_startTime = Time.time;
            m_startPosition = CAMERA.transform.position;

            state = true;

            for(int i = 0; i < m_players.Length;i++)
            {
                m_players[i].goalFlag = true;
            }
        }

        if(count > RESULT_SCENE_COUNT - 120 && count < RESULT_SCENE_COUNT)
        {
            float timeStep = m_time > 0.0f ? (Time.time - m_startTime) / m_time : 1.0f;
            if (m_time > 0.0f)
            {
                timeStep = (Time.time - m_startTime) / m_time;
            }
            else
            {
                timeStep = 1.0f;
            }

            //timeStep = (1 - Mathf.Cos(Time.time * Mathf.PI)) / 2.0f;

            //transform.position = Vector3.Lerp(m_startPosition, m_targetPosition, timeStep);
            CAMERA.transform.position = Lerp(m_startPosition, m_targetPosition, timeStep, Linearity);
        }
        else if (count == RESULT_SCENE_COUNT)
        {
            for (int i = 0; i < 4; i++)
            {
                HELICOPTER[i].GetComponent<Animator>().SetTrigger("Flight2");
            }

            CAMERA.GetComponent<CameraShake>().Shake(1.5f, 0.2f);
        }
        else if(count == RESULT_SCENE_COUNT + 130)
        {
            Fade.FadeIn();
        }
        else if (count > RESULT_SCENE_COUNT + 130 + (Fade.TIME * 60) + 10)
        {
            round.AddCount();
            round.SaveScore(round.GetNowround());
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

    static Vector3 Lerp(Vector3 startPosition, Vector3 targetPosition, float t, Func<float, float> v)
    {
        Vector3 lerpPosition = Vector3.zero;

        lerpPosition = (1 - v(t)) * startPosition + v(t) * targetPosition;

        return lerpPosition;
    }

    static float Linearity(float t)
    {
        float y = t;

        return y;
    }

    static float EaseIn(float t)
    {
        float y = t * t;

        return y;
    }

    static float EaseOut(float t)
    {
        float y = t * (2 - t);

        return y;
    }

    static float Cube(float t)
    {
        float y = t * t * (3 - 2 * t);

        return y;
    }

    static float CosLerp(float t)
    {
        float y = (1 - Mathf.Cos(t * Mathf.PI)) / 2.0f;

        return y;
    }
}
