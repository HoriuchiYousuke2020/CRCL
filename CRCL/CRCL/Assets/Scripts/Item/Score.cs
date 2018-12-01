///2018.10.19
///writer name is Sato Momoya
/// スコアクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyGame/Score", fileName = "Score")]

public class Score : ScriptableObject {

    public string SAVE_KEY;        //セーブするための文字キー
    [SerializeField]
    private  int m_score;          //スコア


	// Use this for initialization
	void Start ()
    {
       // m_score = 0;
        SaveScore(m_score);

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(m_score < 0)
        {
            m_score = 0;
        }
	}

    public void SetScore(int score) //与えられた値(int型)にscoreを書き換える
    {
        m_score = score;
    }

    public int GetScore()          //スコアを取得する
    {
        return m_score;
    }

    public void SaveScore(int score) //スコアをセーブする
    {
        PlayerPrefs.SetInt(SAVE_KEY, score);
        PlayerPrefs.Save();
    }

    public int LoadScore()          //スコアをロードする
    {
        return m_score = PlayerPrefs.GetInt(SAVE_KEY, -1);
    }


    public void SetAndSaveScore(int score) //スコアをセットしつつセーブする
    {
        m_score = (int)score;
        PlayerPrefs.SetInt(SAVE_KEY, m_score);
        PlayerPrefs.Save();
        PlayerPrefs.GetInt(SAVE_KEY);
     
    }

    public void AddScore(int addNum)     //スコアを加算する
    {
        m_score += addNum;
    }

    public void SubScore(int subNum)     //スコアを減算する  
    {
        if(m_score > 50)
        {
            m_score -= subNum;
        }
        else
        {
            m_score = 0;
        }
    
    }

    public void MulScore(int mulNum)    //スコアを掛ける
    {
        m_score *= mulNum;
    }

    public void DibScore(int dibNum)    //スコアを割る
    {
        if (m_score > 0)
        {
            m_score /= dibNum;
        }
    }

    public void Reset()
    {
        m_score = 0;
    }
}
