//2018.10/20
//writer name is Sato Momoya
//ステータス 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyGame/Status", fileName = "Status")]

public class Status : ScriptableObject {

public    float m_speed;   //スピード
public float m_jumpPower;  //ジャンプ力ぅ
public    int   m_power;   //攻撃力
public    int   m_defense; //防御力
public    int m_hp;        //HP
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

public float GetjumpPower()//ジャンプ力を取得する 
    {
        return m_jumpPower;
    }

public void SetJumpPower(float jumpPower)//ジャンプ力をセットする
    {
        m_jumpPower = jumpPower;
    }
public  float GetSpeed() //スピードを取得する
    {
        return m_speed;
    }

public  void SetSpeed(float speed)//スピードをセットする
    {
        m_speed = speed;
    }
    
public int GetPower()//攻撃力を取得する
    {
        return m_power;
    }

public void SetPower(int power)//攻撃力をセットする
    {
        m_power = power;
    }
public int GetDefense()//防御力を取得する
    {
        return m_defense;
    }

public void SetDefence(int defence)//防御力をセットする
    {
        m_defense = defence;
    }
public int GetHp()//HPを取得する
    {
        return m_hp;
    }

public void SetHp(int hp)//HPをセットする
    {
        m_hp = hp;
    }
}
