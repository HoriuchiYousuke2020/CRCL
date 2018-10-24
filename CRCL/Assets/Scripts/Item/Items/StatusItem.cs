﻿///2018. 10/21
///writer name is Sato Momoya
///Item: ステータスにかかわるアイテム
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//速度アップアイテム
public class SpeedUp : Item
{

    // Use this for initialization
    void Start()
    {
        m_itemNumber = 1;
    }

    public override void UseItem(PlayerController player)
    {
        //プレイヤーの速度を1増やす
        //player.GetItem().SetScrollsFlag((int)SCROLLS_STATE.REPLACEAll, true);
        player.GetStatus().SetSpeed(player.GetStatus().GetSpeed() + 2);
        m_isUsedFlag = true;
    }
}

//速度ダウンアイテム
public class SpeedDown : Item
{

    // Use this for initialization
    void Start()
    {
        m_itemNumber = -1;
    }
    public override void UseItem(PlayerController player)
    {
        //プレイヤーの速度を1下げる
        player.GetStatus().SetSpeed(player.GetStatus().GetSpeed() - 1);
        m_isUsedFlag = true;
     
    }
}


//攻撃力アップアイテム
public class AttackUp : Item
{

    // Use this for initialization
    void Start()
    {
        m_itemNumber = 2;
    }

    public override void UseItem(PlayerController player)
    {
        //攻撃力を1上げる
        player.GetStatus().SetPower(player.GetStatus().GetPower() + 1);
        m_isUsedFlag = true;
    }
}

//攻撃力ダウンアイテム
public class AttackDown : Item
{
    private void Start()
    {
        m_itemNumber = -2;
    }

    public override void UseItem(PlayerController player)
    {
        //攻撃力を1下げる
        player.GetStatus().SetPower(player.GetStatus().GetPower() - 1);
        m_isUsedFlag = true;
    }
}


//ジャンプ力アップアイテム
public class JumpUp : Item
{
    private void Start()
    {
        m_itemNumber = 3;
    }

    public override void UseItem(PlayerController player)
    {
        //ジャンプ力を1上げる
        player.GetStatus().SetJumpPower(player.GetStatus().GetjumpPower() * 1.2f);
        m_isUsedFlag = true;
    }

}

//ジャンプ力アップアイテム
public class JumpDown : Item
{
    private void Start()
    {
        m_itemNumber = -3;
    }

    public override void UseItem(PlayerController player)
    {
        //ジャンプ力を1下げる
        player.GetStatus().SetJumpPower(player.GetStatus().GetjumpPower() * 0.8f);
        m_isUsedFlag = true;
    }

}

//Hpアップ(回復)アイテム
public class HpUp :Item
{
    private void Start()
    {
        m_itemNumber = 4;
    }

    public override void UseItem(PlayerController player)
    {
        //HPを1上げる
        player.GetStatus().SetHp(player.GetStatus().GetHp() + 1);
        m_isUsedFlag = true;
    }
}

//Hpダウン(減少)アイテム
public class HpDown : Item
{
    private void Start()
    {
        m_itemNumber = -4;
    }

    public override void UseItem(PlayerController player)
    {
        //HPを1下げる
        player.GetStatus().SetHp(player.GetStatus().GetHp() - 1);
        m_isUsedFlag = true;
    }
}


public class DefenceUp: Item
{
    private void Start()
    {
        m_itemNumber = 5;
    }

    public override void UseItem(PlayerController player)
    {
        //防御力を1上げる
        player.GetStatus().SetDefence(player.GetStatus().GetDefense() + 1);
        m_isUsedFlag = true;
    }
}


public class DefenceDown : Item
{
    private void Start()
    {
        m_itemNumber = -5;
    }

    public override void UseItem(PlayerController player)
    {
        //防御力を1下げる
        player.GetStatus().SetDefence(player.GetStatus().GetDefense() - 1);
        m_isUsedFlag = true;
    }
}

public class ScoreUp : Item
{
    private void Start()
    {
        m_itemNumber = 6;
    }

    public override void UseItem(PlayerController player)
    {
        //スコアをを2倍に
        player.GetScore().MulScore(2);
        m_isUsedFlag = true;
    }

}

//スコアを半分に
public class ScoreDown : Item
{
    private void Start()
    {
        m_itemNumber = -6;
    }

    public override void UseItem(PlayerController player)
    {
        //スコアをを2倍に
        player.GetScore().DibScore(2);
        m_isUsedFlag = true;
    }

}
