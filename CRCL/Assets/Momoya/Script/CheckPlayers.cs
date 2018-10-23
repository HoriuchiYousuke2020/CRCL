// 2018  10/21
//writer name is Sato Momoya
//プレイシーンにいるプレイヤーのポジションを把握し全体アイテムを誰かが使った時に効果をかけるクラスです
//
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class CheckPlayers : MonoBehaviour {
    public PlayerController[] m_players; //プレイヤー
    List<Vector3> m_stockPos;            //プレイヤーのポジションを把握するVector3型の変数
	// Use this for initialization
	void Start ()
    {
        m_stockPos = new List<Vector3>();
        for(int i = 0; i <m_players.Length; i++)//プレイシーンにいる分のプレイヤーを調べる
        {
            m_stockPos.Add(new Vector3(m_players[i].transform.position.x, m_players[i].transform.position.y, m_players[i].transform.position.z));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
     
        for (int i = 0; i < m_players.Length; i++)//プレイシーンにいるプレイヤーのポジションを把握する
        {
            m_stockPos[i] = m_players[i].transform.position;

        }

        for (int i = 0; i < m_players.Length; i++)//もしプレイシーンにいるプレイヤーの誰かが全体アイテムを使ったら
        {
            if (m_players[i].GetItem().GetScrollsFlag((int)Item.SCROLLS_STATE.REPLACE_All) == true)//もし誰かが全入れ替えアイテムを使ったら
            {

                ReplaceAll();//プレイシーンにいるプレイヤー全員のポジションをランダムに入れ替える
               
                m_players[i].GetItem().SetScrollsFlag((int)Item.SCROLLS_STATE.REPLACE_All,false); //アイテムを使ったプレイヤーのフラグをもとに戻す
                m_players[i].GetItem().SetUsedFlag(true);                                        //アイテムを使い終わったことを伝える
            }

            if(m_players[i].GetItem().GetScrollsFlag((int)Item.SCROLLS_STATE.REPLACEA_FIRSTPLACE ) == true)
            {
                ReplaceFirstPlace(m_players[i]);
                m_players[i].GetItem().SetScrollsFlag((int)Item.SCROLLS_STATE.REPLACEA_FIRSTPLACE, false); //アイテムを使ったプレイヤーのフラグをもとに戻す
                m_players[i].GetItem().SetUsedFlag(true);                                        //アイテムを使い終わったことを伝える
            }

        }
     
    }

    public   void ReplaceAll()
    {
        var ary = Enumerable.Range(0, m_players.Length).OrderBy(n => Guid.NewGuid()).Take(m_players.Length).ToArray();
        for (int i = 0; i <m_players.Length; i++)
        {
            m_players[i].transform.position = m_stockPos[ary[i]];
        }
    

    }

public void ReplaceFirstPlace(PlayerController player)
    {
        int rankNum = 0;
        Vector3 copy = player.transform.position;
        Vector3 checkFirstPos = player.transform.position;
        for(int i = 0; i <m_players.Length;i++)
        {
            if(checkFirstPos.y < m_players[i].transform.position.y)
            {
                checkFirstPos =  m_players[i].transform.position;
                rankNum = i;
            }
        }

        player.transform.position = checkFirstPos;
        m_players[rankNum].transform.position = copy;
    }
}
