// 2018  10/21
//writer name is Sato Momoya
//プレイシーンにいるプレイヤーのポジションを把握し全体アイテムを誰かが使った時に効果をかけるクラスです
//
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class CheckPlayers : MonoBehaviour
{
    //プレイヤー
    public PlayerController[] m_players;
    //プレイヤーのポジションを把握するVector3型の変数
    List<Vector3> m_stockPos;
    
    // Use this for initialization
    void Start()
    {
        m_stockPos = new List<Vector3>();
        //プレイシーンにいる分のプレイヤーを調べる
        for (int i = 0; i < m_players.Length; i++)
        {
            m_stockPos.Add(m_players[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プレイシーンにいるプレイヤーのポジションを把握する
        for (int i = 0; i < Setting.PlayerNum; i++)
        {
            m_stockPos[i] = m_players[i].transform.position;
        }
        
        for (int i = 0; i < Setting.PlayerNum; i++)
        {
            //プレイヤーの誰かが全体アイテムを使ったら
            if (m_players[i].GetItem().GetScrollsFlag((int)Item.SCROLLS_STATE.REPLACE_All) == true)
            {
                //プレイヤー全員のポジションをランダムに入れ替える
                ReplaceAll();
                //アイテムを使ったプレイヤーのフラグをもとに戻す
                m_players[i].GetItem().SetScrollsFlag((int)Item.SCROLLS_STATE.REPLACE_All, false);
                //アイテムを使い終わったことを伝える
                m_players[i].GetItem().SetUsedFlag(true);                                        
            }

            if (m_players[i].GetItem().GetScrollsFlag((int)Item.SCROLLS_STATE.REPLACEA_FIRSTPLACE) == true)
            {
                ReplaceFirstPlace(m_players[i]);
                //アイテムを使ったプレイヤーのフラグをもとに戻す
                m_players[i].GetItem().SetScrollsFlag((int)Item.SCROLLS_STATE.REPLACEA_FIRSTPLACE, false);
                //アイテムを使い終わったことを伝える
                m_players[i].GetItem().SetUsedFlag(true);
            }
        }
    }

    /// <summary>
    /// 全員の座標を入れ替える
    /// </summary>
    public void ReplaceAll()
    {
        var ary = Enumerable.Range(0, m_players.Length).OrderBy(n => Guid.NewGuid()).Take(m_players.Length).ToArray();
        for (int i = 0; i < Setting.PlayerNum; i++)
        {
            if (ary[i] != i || i != Setting.PlayerNum)
            {
                m_players[i].transform.position = m_stockPos[ary[i]];
            }
            else
            {
                var tmp = ary[i];
                ary[i] = ary[i + 1];
                ary[i + 1] = tmp;
                m_players[i].transform.position = m_stockPos[ary[i]];
            }

        



        }
    }

    public void ReplaceFirstPlace(PlayerController player)
    {
        int rankNum = -1;
        Vector3 copy = player.transform.position;
        Vector3 checkFirstPos = player.transform.position;
        for (int i = 0; i < m_players.Length; i++)
        {
            if (checkFirstPos.y < m_players[i].transform.position.y)
            {
                checkFirstPos = m_players[i].transform.position;
                rankNum = i;
            }
        }
        if(rankNum != -1)
        {
           player.transform.position = checkFirstPos;
           m_players[rankNum].transform.position = copy;
        }
     
    }
}
