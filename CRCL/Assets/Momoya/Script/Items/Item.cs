///2018.10/21
///writer name is Sato Momoya
///アイテム
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected int m_itemNumber; //アイテム番号
    protected bool[] m_scrollsFlag = new bool[(int)SCROLLS_STATE.ANY]; //巻物フラグ
    protected bool m_isUsedFlag;

    // Use this for initialization
    public enum SCROLLS_STATE //巻物を使った時のステート
    {
        NORMAL,
        REPLACE_All,//位置全入れ替え
        REPLACEA_FIRSTPLACE,//1位と入れ替え
        ANY
    }

    public Item()
    {
        m_isUsedFlag = false;
        for (int i = 0; i < (int)SCROLLS_STATE.ANY; i++)
        {
            m_scrollsFlag[i] = false;
        }
    }

    private void Start()
    {
        m_itemNumber = 0;
        m_isUsedFlag = false;
        for (int i = 0; i < (int)SCROLLS_STATE.ANY; i++)
        {
            m_scrollsFlag[i] = false;
        }

    }
    //コンストラクタ


    // Update is called once per frame
    public virtual void UseItem(PlayerController player)//ステータスを変化させるアイテム
    {
        // ここにアイテムの効果を記入
    }



    public int GetItemNumber() //アイテム番号を取得する
    {
        return m_itemNumber;
    }

    public bool GetScrollsFlag(int i) //指定された巻物フラグを取得する
    {
        return m_scrollsFlag[i];
    }

    public void SetScrollsFlag(int i, bool flag)//指定された巻物フラグをセットする
    {
        m_scrollsFlag[i] = flag;
    }

    public bool GetUsedFlag()
    {
        return m_isUsedFlag;
    }

    public void SetUsedFlag(bool flag)
    {
        m_isUsedFlag = flag;
    }
}
