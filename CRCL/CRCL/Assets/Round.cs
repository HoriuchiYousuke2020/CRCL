using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyGame/Round", fileName = "Round")]
public class Round : ScriptableObject
{
    public string SAVE_KEY;        //セーブするための文字キー
    [SerializeField]
    int nowRound;
    // Use this for initialization
    void Start()
    {
        SaveScore(nowRound);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveScore(int score) //スコアをセーブする
    {
        PlayerPrefs.SetInt(SAVE_KEY, score);
        PlayerPrefs.Save();
    }

    public int LoadScore()          //スコアをロードする
    {
        return nowRound = PlayerPrefs.GetInt(SAVE_KEY, -1);
    }

    public void SetAndSaveScore(int score) //スコアをセットしつつセーブする
    {
        nowRound = (int)score;
        PlayerPrefs.SetInt(SAVE_KEY, nowRound);
        PlayerPrefs.Save();
        PlayerPrefs.GetInt(SAVE_KEY);

    }

    public int GetNowround()
    {
        return nowRound;
    }

    public void AddCount()
    {
        nowRound++;
    }

    public void Reset()
    {
        nowRound = 0;
    }
}
